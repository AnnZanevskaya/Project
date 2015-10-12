using System.Linq;
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
                Files = userEntity.Files.Select(file => file.ToDalFile()).ToList(),
                Roles = userEntity.Roles.Select(role => role.ToDalRole()).ToList(),
                Profile = userEntity.Profile.ToDalProfile()
            };
        }
        public static DalProfile ToDalProfile(this ProfileEntity profileEntity)
        {
            return new DalProfile()
            {
               Id = profileEntity.Id,
               Age = profileEntity.Age,
               FirstName = profileEntity.FirstName,
               LastName = profileEntity.LastName,
               LastUpdate = profileEntity.LastUpdate
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
                CreationTime = fileEntity.CreationTime,
                UserId = fileEntity.UserId                
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
        public static DalRole ToDalRole(this RoleEntity roleEntity)
        {
            if (roleEntity == null) return null;
            return new DalRole
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name
            };
        }
        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                Id = dalUser.Id,
                Email = dalUser.Email,
                Password = dalUser.Password,
                Files = dalUser.Files.Select(file => file.ToBllFile()).ToList(),
                Roles = dalUser.Roles.Select(role => role.ToBllRole()).ToList(),
                Profile = dalUser.Profile.ToBllProfile()
            };
        }
        public static ProfileEntity ToBllProfile(this DalProfile dalProfile)
        {
            return new ProfileEntity()
            {
                Id = dalProfile.Id,
                Age = dalProfile.Age,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                LastUpdate = dalProfile.LastUpdate
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
                UserId = dalFile.UserId,
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
        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            if (dalRole == null) return null;
            return new RoleEntity
            {
                Id = dalRole.Id,
                Name = dalRole.Name
            };
        }
    }
}
