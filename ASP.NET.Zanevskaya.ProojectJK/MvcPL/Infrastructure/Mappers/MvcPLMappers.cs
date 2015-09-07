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
                Password = userEntity.Password,
                Role = (Role)userEntity.RoleId
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
                CreationTime = fileEntity.CreationTime
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
        public static UserEntity ToBllUser(this UserViewModel userViewModel)
        {
            return new UserEntity()
            {
                Id = userViewModel.Id,
                Email = userViewModel.Email,
                Password = userViewModel.Password,
                RoleId = (int)userViewModel.Role
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
                CreationTime = fileViewModel.CreationTime
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
    }
}