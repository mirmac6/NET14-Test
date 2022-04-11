using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFinalTest.Controllers;
using WebAppFinalTest.Models;
using WebAppFinalTest.Models.DTO;
using WebAppFinalTest.Repository.Interfaces;
using Xunit;

namespace WebAppFinalTestTest.Controllers
{
    public class albumiControllerTest
    {
        [Fact]
        public void GetAlbum_ValidAlbum_ReturnsAlbum()
        {
            // Arrange
            Album album = new Album() { Id = 2, Name = "neko ime", Genre = "zanr", Year = 2005, Sold = 35, BandId = 1, Band = new Band() { Id = 1, Name = "Band1", Year = 2008 } };
            AlbumDTO albumDTO = new AlbumDTO() { Id = 2, Name = "neko ime", Genre = "zanr", Year = 2005, Sold = 35, Band = "Band1" };

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new AlbumProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);
            var mockRepository = new Mock<IAlbumRepository>();

            mockRepository.Setup(x => x.GetById(2)).Returns(album);



            var controller = new albumiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.GetAlbum(2) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);
            AlbumDTO dtoResult = (AlbumDTO)actionResult.Value;

            Assert.Equal(albumDTO.Id, dtoResult.Id);
            Assert.Equal(albumDTO.Name, dtoResult.Name);
            Assert.Equal(albumDTO.Genre, dtoResult.Genre);
            Assert.Equal(albumDTO.Year, dtoResult.Year);
            Assert.Equal(albumDTO.Sold, dtoResult.Sold);
            Assert.Equal(albumDTO.Band, dtoResult.Band);
        }

        [Fact]
        public void PutAlbum_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            Album album = new Album() { Id = 2, Name = "neko ime", Genre = "zanr", Year = 2005, Sold = 35, BandId = 1, Band = new Band() { Id = 1, Name = "Band1", Year = 2008 } };
            var mockRepository = new Mock<IAlbumRepository>();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new AlbumProfile()));

            IMapper mapper = new Mapper(mapperConfiguration);
            var controller = new albumiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.PutAlbum(22, album) as BadRequestResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void DeleteAlbum_InvalidId_ReturnsNotFound()
        {
            // Arrange
            Album album = new Album() { Id = 2, Name = "neko ime", Genre = "zanr", Year = 2005, Sold = 35, BandId = 1, Band = new Band() { Id = 1, Name = "Band1", Year = 2008 } };
            var mockRepository = new Mock<IAlbumRepository>();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new AlbumProfile()));

            IMapper mapper = new Mapper(mapperConfiguration);
            var controller = new albumiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.DeleteAlbum(22) as NotFoundResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void GetPretraga_ValidTakmicari_ReturnsCollection()
        {
            // Arrange
            // Arrange
            List<Album> albums = new List<Album>() {
                new Album() { Id = 2, Name = "neko ime", Genre = "zanr", Year = 2005, Sold = 35, BandId = 1, Band = new Band() { Id = 1, Name = "Band1", Year = 2008 }  },
                new Album() { Id = 3, Name = "bla bla", Genre = "zanr2", Year = 2001, Sold = 3, BandId = 2, Band = new Band() { Id = 2, Name = "Band2", Year = 2002 }  }
            };

            List<AlbumDTO> albumsDTO = new List<AlbumDTO>() {
                new AlbumDTO() { Id = 2, Name = "neko ime", Genre = "zanr", Year = 2005, Sold = 35, Band = "Band1"},
                new AlbumDTO() { Id = 3, Name = "bla bla", Genre = "zanr2", Year = 2001, Sold = 3, Band = "Band2" }
            };
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new AlbumProfile()));
            var mockRepository = new Mock<IAlbumRepository>();
            mockRepository.Setup(x => x.FilterByYear(2000, 2010)).Returns(albums);

            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new albumiController(mockRepository.Object, mapper);
            AlbumDTOSearch ato = new AlbumDTOSearch() { Max = 2010, Min = 2000 };
            // Act
            var actionResult = controller.GetAlbumsByYear(ato) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            List<AlbumDTO> dtoResult = (List<AlbumDTO>)actionResult.Value;

            for (int i = 0; i < dtoResult.Count; i++)
            {
                Assert.Equal(albumsDTO[i].Id, dtoResult[i].Id);
                Assert.Equal(albumsDTO[i].Name, dtoResult[i].Name);
                Assert.Equal(albumsDTO[i].Genre, dtoResult[i].Genre);
                Assert.Equal(albumsDTO[i].Sold, dtoResult[i].Sold);
                Assert.Equal(albumsDTO[i].Year, dtoResult[i].Year);
                Assert.Equal(albumsDTO[i].Band, dtoResult[i].Band);
            }
        }
    }
}
