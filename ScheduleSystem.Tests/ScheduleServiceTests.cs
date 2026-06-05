using AutoMapper;
using Moq;
using ScheduleSystem.BLL.Exceptions;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Mapping;
using ScheduleSystem.BLL.Models;
using ScheduleSystem.BLL.Services;
using ScheduleSystem.DAL.Entities;
using ScheduleSystem.DAL.Interfaces;

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
    public async Task CreateAsync_ValidDto_CallsSaveAndReturnsDto()
    {
        // Arrange
        // TODO: setup mocks, prepare dto

        // Act
        // TODO: call _scheduleService.CreateAsync(dto)

        // Assert
        // TODO: verify _mockUow.Verify(u => u.SaveAsync(), Times.Once)
        throw new NotImplementedException();
    }

    [Fact]
    public async Task CreateAsync_ConflictingEntry_ThrowsScheduleConflictException()
    {
        // Arrange
        // TODO: setup _mockScheduleRepo HasConflictAsync to return true

        // Act & Assert
        // TODO: await Assert.ThrowsAsync<ScheduleConflictException>(...)
        throw new NotImplementedException();
    }

    [Fact]
    public async Task DeleteAsync_NonExistentId_ThrowsNotFoundException()
    {
        // Arrange
        // TODO: setup _mockScheduleRepo GetByIdAsync to return null

        // Act & Assert
        // TODO: await Assert.ThrowsAsync<NotFoundException>(...)
        throw new NotImplementedException();
    }

    [Fact]
    public async Task DeleteAsync_ValidId_CallsDeleteAndSave()
    {
        // Arrange
        // TODO: setup mock to return a valid entry

        // Act
        // TODO: call _scheduleService.DeleteAsync(id)

        // Assert
        // TODO: verify Delete and SaveAsync were called
        throw new NotImplementedException();
    }

    [Fact]
    public async Task GetByGroupAsync_ValidId_ReturnsEntries()
    {
        // Arrange
        // TODO: setup mock to return a list of entries

        // Act
        // TODO: call _scheduleService.GetByGroupAsync(groupId)

        // Assert
        // TODO: Assert.NotEmpty(result)
        throw new NotImplementedException();
    }

    [Fact]
    public async Task GetByGroupAsync_EmptyResult_ReturnsEmptyList()
    {
        // Arrange
        // TODO: setup mock to return empty list

        // Act
        // TODO: call _scheduleService.GetByGroupAsync(groupId)

        // Assert
        // TODO: Assert.Empty(result)
        throw new NotImplementedException();
    }

    [Fact]
    public async Task GetFilteredAsync_WithFilters_PassesCorrectParamsToRepo()
    {
        // Arrange
        // TODO: prepare filter dto

        // Act
        // TODO: call _scheduleService.GetFilteredAsync(filter)

        // Assert
        // TODO: verify repo was called with correct params
        throw new NotImplementedException();
    }

    [Fact]
    public async Task UpdateAsync_NonExistentId_ThrowsNotFoundException()
    {
        // Arrange
        // TODO: setup mock to return null

        // Act & Assert
        // TODO: await Assert.ThrowsAsync<NotFoundException>(...)
        throw new NotImplementedException();
    }
}
