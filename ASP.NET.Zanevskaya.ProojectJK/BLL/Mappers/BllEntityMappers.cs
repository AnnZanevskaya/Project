using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                Password = userEntity.Password,
                RoleId = userEntity.RoleId
            };
        }
        public static DalFile ToDalFile(this FileEntity fileEntity)
        {
            return new DalFile()
            {
                Id = fileEntity.Id,
                Description = fileEntity.Description,
                Name = fileEntity.Name,
                FileType = fileEntity.FileType,
                Rating = fileEntity.Rating,
                Path = fileEntity.Path,
                CreationTime = fileEntity.CreationTime
               // UserId = fileEntity.UserId                
            };
        }
        public static DalComment ToDalComment(this CommentEntity commentsEntity)
        {
            return new DalComment()
            {
                Id = commentsEntity.Id,
                UserName = commentsEntity.UserName,
                FileId = commentsEntity.FileId,
                Messange = commentsEntity.Messange,
                CreationTime = commentsEntity.CreationTime              
            };
        }
        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                Id = dalUser.Id,
                Email = dalUser.Email,
                Password = dalUser.Password,
                RoleId = dalUser.RoleId
            };
        }
        public static FileEntity ToBllFile(this DalFile dalFile)
        {
            return new FileEntity()
            {
                Id = dalFile.Id,
                Description = dalFile.Description,
                FileType = dalFile.FileType,
                Rating = dalFile.Rating,
                Name = dalFile.Name,
               // UserId = dalFile.UserId,
                Path = dalFile.Path,
                CreationTime = dalFile.CreationTime
                
            };
        }
        public static CommentEntity ToBllComment(this DalComment dalComment)
        {
            return new CommentEntity()
            {
                Id = dalComment.Id,
                UserName = dalComment.UserName,
                FileId = dalComment.FileId,
                Messange = dalComment.Messange,
                CreationTime = dalComment.CreationTime
            };
        }
    }
}
