using AutoMapper;
using Moq;
using ScheduleSystem.BLL.Exceptions;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Mapping;
using ScheduleSystem.BLL.Models;
using ScheduleSystem.BLL.Services;
using ScheduleSystem.DAL.Entities;
using ScheduleSystem.DAL.Interfaces;
using Xunit;

namespace ScheduleSystem.Tests;

public class ScheduleServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUow;
    private readonly Mock<IScheduleRepository> _mockScheduleRepo;
    private readonly IMapper _mapper;
    private readonly IScheduleService _scheduleService;

    public ScheduleServiceTests()
    {
        _mockUow = new Mock<IUnitOfWork>();
        _mockScheduleRepo = new Mock<IScheduleRepository>();
        _mockUow.Setup(u => u.ScheduleEntries).Returns(_mockScheduleRepo.Object);

        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();

        _scheduleService = new ScheduleService(_mockUow.Object, _mapper);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsDto()
    {
        // Arrange
        var entry = new ScheduleEntry
        {
            Id = 1,
            GroupId = 1,
            TeacherId = 1,
            ClassroomId = 1,
            SubjectId = 1
        };
        _mockScheduleRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entry);

        // Act
        var result = await _scheduleService.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistentId_ThrowsNotFoundException()
    {
        // Arrange
        _mockScheduleRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((ScheduleEntry?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _scheduleService.GetByIdAsync(99));
    }

    [Fact]
    public async Task GetByGroupAsync_ValidId_ReturnsEntries()
    {
        // Arrange
        var entries = new List<ScheduleEntry> { new() { Id = 1, GroupId = 1 } };
        _mockScheduleRepo.Setup(r => r.GetByGroupAsync(1)).ReturnsAsync(entries);

        // Act
        var result = await _scheduleService.GetByGroupAsync(1);

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetByGroupAsync_NoEntries_ReturnsEmptyList()
    {
        // Arrange
        _mockScheduleRepo.Setup(r => r.GetByGroupAsync(1))
            .ReturnsAsync(new List<ScheduleEntry>());

        // Act
        var result = await _scheduleService.GetByGroupAsync(1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task CreateAsync_ValidDto_CallsSaveAndReturnsDto()
    {
        // Arrange
        var dto = new ScheduleEntryDto
        { SubjectId = 1, TeacherId = 1, GroupId = 1, ClassroomId = 1 };
        _mockUow.Setup(u => u.Subjects.GetByIdAsync(1))
            .ReturnsAsync(new Subject { Id = 1 });
        _mockUow.Setup(u => u.Teachers.GetByIdAsync(1))
            .ReturnsAsync(new Teacher { Id = 1 });
        _mockUow.Setup(u => u.Groups.GetByIdAsync(1))
            .ReturnsAsync(new Group { Id = 1 });
        _mockUow.Setup(u => u.Classrooms.GetByIdAsync(1))
            .ReturnsAsync(new Classroom { Id = 1 });
        _mockScheduleRepo.Setup(r => r.HasConflictAsync(It.IsAny<ScheduleEntry>()))
            .ReturnsAsync(false);

        // Act
        var result = await _scheduleService.CreateAsync(dto);

        // Assert
        _mockUow.Verify(u => u.SaveAsync(), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task CreateAsync_ConflictingEntry_ThrowsScheduleConflictException()
    {
        // Arrange
        var dto = new ScheduleEntryDto
        { SubjectId = 1, TeacherId = 1, GroupId = 1, ClassroomId = 1 };
        _mockUow.Setup(u => u.Subjects.GetByIdAsync(1))
            .ReturnsAsync(new Subject { Id = 1 });
        _mockUow.Setup(u => u.Teachers.GetByIdAsync(1))
            .ReturnsAsync(new Teacher { Id = 1 });
        _mockUow.Setup(u => u.Groups.GetByIdAsync(1))
            .ReturnsAsync(new Group { Id = 1 });
        _mockUow.Setup(u => u.Classrooms.GetByIdAsync(1))
            .ReturnsAsync(new Classroom { Id = 1 });
        _mockScheduleRepo.Setup(r => r.HasConflictAsync(It.IsAny<ScheduleEntry>()))
            .ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ScheduleConflictException>(() =>
            _scheduleService.CreateAsync(dto));
    }

    [Fact]
    public async Task CreateAsync_NonExistentGroupId_ThrowsNotFoundException()
    {
        // Arrange
        var dto = new ScheduleEntryDto
        { SubjectId = 1, TeacherId = 1, GroupId = 99, ClassroomId = 1 };
        _mockUow.Setup(u => u.Subjects.GetByIdAsync(1))
            .ReturnsAsync(new Subject { Id = 1 });
        _mockUow.Setup(u => u.Teachers.GetByIdAsync(1))
            .ReturnsAsync(new Teacher { Id = 1 });
        _mockUow.Setup(u => u.Groups.GetByIdAsync(99))
            .ReturnsAsync((Group?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _scheduleService.CreateAsync(dto));
    }

    [Fact]
    public async Task DeleteAsync_ValidId_CallsDeleteAndSave()
    {
        // Arrange
        var entry = new ScheduleEntry { Id = 1 };
        _mockScheduleRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entry);

        // Act
        await _scheduleService.DeleteAsync(1);

        // Assert
        _mockScheduleRepo.Verify(r => r.Delete(entry), Times.Once);
        _mockUow.Verify(u => u.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_NonExistentId_ThrowsNotFoundException()
    {
        // Arrange
        _mockScheduleRepo.Setup(r => r.GetByIdAsync(99))
            .ReturnsAsync((ScheduleEntry?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _scheduleService.DeleteAsync(99));
    }

    [Fact]
    public async Task UpdateAsync_NonExistentId_ThrowsNotFoundException()
    {
        // Arrange
        var dto = new ScheduleEntryDto { Id = 99 };
        _mockScheduleRepo.Setup(r => r.GetByIdAsync(99))
            .ReturnsAsync((ScheduleEntry?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _scheduleService.UpdateAsync(dto));
    }
}
