using HRMS.Application.Interfaces.Repositories;
using HRMS.Application.Services;
using HRMS.Domain.Entities;
using Xunit;
using NSubstitute;
using HRMS.Application.DTOs.V1.Department;
using Microsoft.Extensions.Logging;
using HRMS.Application.Enums;

namespace HRMS.Application.UnitTests.Services
{
    public class DepartmentServiceTests
    {
        private readonly CancellationToken _ct = CancellationToken.None;
        private readonly IDepartmentRepository _repo;
        private readonly ILogger<DepartmentService> _logger;
        private readonly DepartmentService _service;
        public DepartmentServiceTests(){
            _repo = Substitute.For<IDepartmentRepository>();
            _logger = Substitute.For<ILogger<DepartmentService>>();
            _service = new DepartmentService(_repo, _logger);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnDepartmentDto_WhenIdIsValidAndExists(){
            //Arrange
            var dept = new Department{Id = 1, Name = "Sales", IsActive = true};
            _repo.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Department?>(dept));
            //Act
            var res = await _service.GetByIdAsync(1, _ct);
            //Assert
            await _repo.Received(1).GetByIdAsync(1, Arg.Any<CancellationToken>());
            Assert.True(res.IsSuccess);
            Assert.NotNull(res.Value);
            Assert.Equal(1, res.Value.Id);
            Assert.Equal("Sales", res.Value.Name);
            Assert.True(res.Value.IsActive);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetByIdAsync_ShouldReturnResultFailure_WhenIdIsInvalid(int id){
            //Arrange
            //Act
            var res = await _service.GetByIdAsync(id, _ct);
            //Assert
            await _repo.DidNotReceive().GetByIdAsync(id, Arg.Any<CancellationToken>());
            Assert.Null(res.Value);
            Assert.NotNull(res.Error);
            Assert.False(res.IsSuccess);
            Assert.Equal(enError.BadRequest, res.Error.Code);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnResultFailure_WhenIdIsValidButDepartmentNotFound(){
            //Arrange
            _repo.GetByIdAsync(99, Arg.Any<CancellationToken>()).Returns((Department?)null);
            //Act
            var res = await _service.GetByIdAsync(99, _ct);
            //Assert
            await _repo.Received(1).GetByIdAsync(99, Arg.Any<CancellationToken>());
            Assert.False(res.IsSuccess);
            Assert.Null(res.Value);
            Assert.NotNull(res.Error);
            Assert.Equal(enError.NotFound, res.Error.Code);
        }
    }
}