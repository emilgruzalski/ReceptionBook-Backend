using Entities.Models;
using Shared.RequestFeatures;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
    }

    [Fact]
    public async Task GetAllReservationsAsync_ReturnsNonEmptyCollection_WithCorrectMetaData()
    {
        // Arrange
        var mockReservationsWithMetaData = new PagedList<Reservation>(
            new List<Reservation> { new Reservation(), new Reservation() },
            totalCount: 2,
            pageNumber: 1,
            pageSize: 10
        );
        var mockMetaData = new MetaData
        {
            TotalCount = 2,
            PageSize = 10,
            CurrentPage = 1,
            TotalPages = 1,
            HasNext = false,
            HasPrevious = false
        };
        _mockRepo.Setup(repo => repo.Reservation.GetAllReservationsAsync(It.IsAny<bool>(), It.IsAny<ReservationParameters>()))
            .ReturnsAsync(mockReservationsWithMetaData); // Mocking method call to return a list of reservations
        var service = new ReservationService(_mockRepo.Object, _mockLogger.Object, _mapper);

        // Act
        var (reservations, metaData) = await service.GetAllReservationsAsync(false, new ReservationParameters());

        // Assert
        Assert.NotNull(reservations);
        Assert.NotEmpty(reservations);
        Assert.Equal(2, reservations.Count());
        Assert.Equal(mockMetaData.TotalCount, metaData.TotalCount);
        Assert.Equal(mockMetaData.PageSize, metaData.PageSize);
    }

}