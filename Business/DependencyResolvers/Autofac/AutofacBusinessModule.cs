using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Security.JWT;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // News
            builder.RegisterType<NewsManager>().As<INewsService>().SingleInstance();
            builder.RegisterType<EfNewsDal>().As<INewsDal>().SingleInstance();

            // Category
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            // Author
            builder.RegisterType<AuthorManager>().As<IAuthorService>().SingleInstance();
            builder.RegisterType<EfAuthorDal>().As<IAuthorDal>().SingleInstance();

            // User & Auth
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>().SingleInstance();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();
            
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();

            // Comment
            builder.RegisterType<CommentManager>().As<ICommentService>().SingleInstance();
            builder.RegisterType<EfCommentDal>().As<ICommentDal>().SingleInstance();

            // Favorite
            builder.RegisterType<FavoriteManager>().As<IFavoriteService>().SingleInstance();
            builder.RegisterType<EfFavoriteDal>().As<IFavoriteDal>().SingleInstance();

            // File Helper
            builder.RegisterType<FileHelperManager>().As<IFileHelper>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
} 