using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ZEST_Student_Management_System.DTOs;
using ZEST_Student_Management_System.Models;
using ZEST_Student_Management_System.Repositories.Interface;
using ZEST_Student_Management_System.Services;

namespace ZEST_Student_Management_System.Tests.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class StudentServiceTests
    {
        /// <summary>
        /// Gets all students asynchronous when students exist returns all students.
        /// </summary>
        [Fact]
        public async Task GetAllStudentsAsync_WhenStudentsExist_ReturnsAllStudents()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Alice", Email = "a@x.com", Age = 20, Course = "Math" },
                new Student { Id = 2, Name = "Bob", Email = "b@x.com", Age = 22, Course = "CS" }
            };

            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(students);

            var service = new StudentService(mockRepo.Object);

            // Act
            var result = await service.GetAllStudentsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        /// <summary>
        /// Gets all students asynchronous when no students exist returns empty collection.
        /// </summary>
        [Fact]
        public async Task GetAllStudentsAsync_WhenNoStudentsExist_ReturnsEmptyCollection()
        {
            // Arrange
            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Student>());

            var service = new StudentService(mockRepo.Object);

            // Act
            var result = await service.GetAllStudentsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        /// <summary>
        /// Gets the student by identifier asynchronous when student exists returns student.
        /// </summary>
        [Fact]
        public async Task GetStudentByIdAsync_WhenStudentExists_ReturnsStudent()
        {
            // Arrange
            var student = new Student { Id = 1, Name = "Alice", Email = "a@x.com", Age = 20, Course = "Math" };
            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(student);

            var service = new StudentService(mockRepo.Object);

            // Act
            var result = await service.GetStudentByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Alice", result.Name);
            mockRepo.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        /// <summary>
        /// Gets the student by identifier asynchronous when student does not exist returns null.
        /// </summary>
        [Fact]
        public async Task GetStudentByIdAsync_WhenStudentDoesNotExist_ReturnsNull()
        {
            // Arrange
            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Student?)null);

            var service = new StudentService(mockRepo.Object);

            // Act
            var result = await service.GetStudentByIdAsync(99);

            // Assert
            Assert.Null(result);
            mockRepo.Verify(r => r.GetByIdAsync(99), Times.Once);
        }

        /// <summary>
        /// Adds the student asynchronous with valid dto creates and returns student and calls repository once.
        /// </summary>
        [Fact]
        public async Task AddStudentAsync_WithValidDto_CreatesAndReturnsStudent_AndCallsRepositoryOnce()
        {
            // Arrange
            var dto = new CreateStudentDto { Name = "Charlie", Email = "c@x.com", Age = 25, Course = "Physics" };
            Student? captured = null;

            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Student>()))
                .Callback<Student>(s => captured = s)
                .ReturnsAsync((Student s) => s);

            var service = new StudentService(mockRepo.Object);

            // Act
            var result = await service.AddStudentAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Email, result.Email);
            Assert.Equal(dto.Age, result.Age);
            Assert.Equal(dto.Course, result.Course);
            Assert.True((DateTime.UtcNow - result.CreatedDate).TotalSeconds < 5);

            Assert.NotNull(captured);
            Assert.Equal(dto.Name, captured.Name);
            Assert.Equal(dto.Email, captured.Email);
            Assert.Equal(dto.Age, captured.Age);
            Assert.Equal(dto.Course, captured.Course);

            mockRepo.Verify(r => r.AddAsync(It.IsAny<Student>()), Times.Exactly(1));
        }

        /// <summary>
        /// Updates the student asynchronous when student exists returns updated student and calls repository once.
        /// </summary>
        [Fact]
        public async Task UpdateStudentAsync_WhenStudentExists_ReturnsUpdatedStudent_AndCallsRepositoryOnce()
        {
            // Arrange
            var dto = new UpdateStudentDto { Name = "Dave", Email = "d@x.com", Age = 30, Course = "Chemistry" };
            var updated = new Student { Id = 5, Name = dto.Name, Email = dto.Email, Age = dto.Age, Course = dto.Course };

            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(r => r.UpdateAsync(It.Is<Student>(s => s.Id == 5)))
                .ReturnsAsync(updated);

            var service = new StudentService(mockRepo.Object);

            // Act
            var result = await service.UpdateStudentAsync(5, dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Id);
            Assert.Equal(dto.Name, result.Name);
            mockRepo.Verify(r => r.UpdateAsync(It.Is<Student>(s => s.Id == 5 && s.Name == dto.Name && s.Email == dto.Email && s.Age == dto.Age && s.Course == dto.Course)), Times.Once);
        }

        /// <summary>
        /// Updates the student asynchronous when student does not exist returns null.
        /// </summary>
        [Fact]
        public async Task UpdateStudentAsync_WhenStudentDoesNotExist_ReturnsNull()
        {
            // Arrange
            var dto = new UpdateStudentDto { Name = "Eve", Email = "e@x.com", Age = 28, Course = "Biology" };
            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Student>())).ReturnsAsync((Student?)null);

            var service = new StudentService(mockRepo.Object);

            // Act
            var result = await service.UpdateStudentAsync(10, dto);

            // Assert
            Assert.Null(result);
            mockRepo.Verify(r => r.UpdateAsync(It.Is<Student>(s => s.Id == 10 && s.Name == dto.Name)), Times.Once);
        }

        /// <summary>
        /// Deletes the student asynchronous when deletion succeeds returns true and calls repository once.
        /// </summary>
        [Fact]
        public async Task DeleteStudentAsync_WhenDeletionSucceeds_ReturnsTrue_AndCallsRepositoryOnce()
        {
            // Arrange
            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(r => r.DeleteAsync(3)).ReturnsAsync(true);

            var service = new StudentService(mockRepo.Object);

            // Act
            var result = await service.DeleteStudentAsync(3);

            // Assert
            Assert.True(result);
            mockRepo.Verify(r => r.DeleteAsync(3), Times.Once);
        }

        /// <summary>
        /// Deletes the student asynchronous when student does not exist returns false and calls repository once.
        /// </summary>
        [Fact]
        public async Task DeleteStudentAsync_WhenStudentDoesNotExist_ReturnsFalse_AndCallsRepositoryOnce()
        {
            // Arrange
            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(r => r.DeleteAsync(99)).ReturnsAsync(false);

            var service = new StudentService(mockRepo.Object);

            // Act
            var result = await service.DeleteStudentAsync(99);

            // Assert
            Assert.False(result);
            mockRepo.Verify(r => r.DeleteAsync(99), Times.Once);
        }
    }
}
