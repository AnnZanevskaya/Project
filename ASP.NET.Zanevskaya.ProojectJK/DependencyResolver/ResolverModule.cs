using System.Data.Entity;
using BLL.Interface.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface.Repository;
using DAL.Interface.DTO;
using Ninject;
using Ninject.Web.Common;
using ORM;
using BLL.Interface.Entities;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel);
        }


        private static void Configure(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<DbContext>().To<EntityModel>().InRequestScope();
            kernel.Bind<IService<UserEntity>>().To<UserService>();
            kernel.Bind<IService<ProfileEntity>>().To<ProfileService>();
            kernel.Bind<IService<FileEntity>>().To<FileService>();
            kernel.Bind<IService<CommentEntity>>().To<CommentService>();
            kernel.Bind<IService<RoleEntity>>().To<RoleService>();
            kernel.Bind<IRepository<DalUser>>().To<UserRepository>();
            kernel.Bind<IRepository<DalFile>>().To<FileRepository>();
            kernel.Bind<IRepository<DalComment>>().To<CommentRepository>();
            kernel.Bind<IRepository<DalRole>>().To<RoleRepository>();
            kernel.Bind<IRepository<DalProfile>>().To<ProfileRepository>();
        }
    }
}