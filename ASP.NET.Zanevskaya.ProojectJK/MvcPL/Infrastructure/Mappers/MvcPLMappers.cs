using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserViewModel()
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                Password = userEntity.Password
            };
        }
        public static ProfileViewModel ToMvcProfile(this ProfileEntity profileEntity)
        {
            return new ProfileViewModel()
            {
                Id = profileEntity.Id,
                FirstName = profileEntity.FirstName,
                LastName = profileEntity.LastName,
                Age = profileEntity.Age,
                LastUpdate = profileEntity.LastUpdate
            };
        }
        public static FileViewModel ToMvcFile(this FileEntity fileEntity)
        {
            return new FileViewModel()
            {
                Id = fileEntity.Id,
                FileName = fileEntity.Name,
                Path = fileEntity.Path,
                Description = fileEntity.Description,
                Rating = fileEntity.Rating,
                FileType = fileEntity.FileType,
                CreationTime = fileEntity.CreationTime,
                UserId = fileEntity.UserId
            };
        }
        public static CommentViewModel ToMvcComment(this CommentEntity commentEntity)
        {
            return new CommentViewModel()
            {
                Id = commentEntity.Id,
                UserName = commentEntity.UserName,
                FileId = commentEntity.FileId,
                Messange = commentEntity.Messange,
                CreationTime = commentEntity.CreationTime
            };
        }
        public static RoleViewModel ToMvcComment(this RoleEntity roleEntity)
        {
            return new RoleViewModel()
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name
            };
        }
        public static UserEntity ToBllUser(this UserViewModel userViewModel)
        {
            return new UserEntity()
            {
                Id = userViewModel.Id,
                Email = userViewModel.Email,
                Password = userViewModel.Password
            };
        }
        public static FileEntity ToBllFile(this FileViewModel fileViewModel)
        {
            return new FileEntity()
            {
                Id = fileViewModel.Id,
                Name = fileViewModel.FileName,
                Path = fileViewModel.Path,
                Description = fileViewModel.Description,
                Rating = fileViewModel.Rating,
                FileType = fileViewModel.FileType,
                CreationTime = fileViewModel.CreationTime,
                UserId = fileViewModel.UserId
            };
        }
        public static CommentEntity ToBllComment(this CommentViewModel commentViewModel)
        {
            return new CommentEntity()
            {
                Id = commentViewModel.Id,
                UserName = commentViewModel.UserName,
                FileId = commentViewModel.FileId,
                Messange = commentViewModel.Messange,
                CreationTime = commentViewModel.CreationTime
            };
        }
        public static RoleEntity ToBllRole(this RoleViewModel roleViewModel)
        {
            return new RoleEntity()
            {
                Id = roleViewModel.Id,
                Name = roleViewModel.Name
            };
        }
        public static ProfileEntity ToBllProfile(this ProfileViewModel profileViewModel)
        {
            return new ProfileEntity()
            {
                Id = profileViewModel.Id,
                FirstName = profileViewModel.FirstName,
                LastName = profileViewModel.LastName,
                Age = profileViewModel.Age,
                LastUpdate = profileViewModel.LastUpdate
            };
        }
    }
}