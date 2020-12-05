using AutoMapper;
using PezeshkGit.Dtos;
using PezeshkGit.Models;

namespace PezeshkGit.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Admin, AdminDto>();
            Mapper.CreateMap<AdminDto, Admin>();

            Mapper.CreateMap<Category, CategoryDto>();
            Mapper.CreateMap<CategoryDto, Category>();

            Mapper.CreateMap<Comment, CommentDto>();
            Mapper.CreateMap<CommentDto, Comment>();

            Mapper.CreateMap<Gallery, GalleryDto>();
            Mapper.CreateMap<GalleryDto, Gallery>();

            Mapper.CreateMap<Message, MessageDto>();
            Mapper.CreateMap<MessageDto, Message>();

            Mapper.CreateMap<Post, PostDto>();
            Mapper.CreateMap<PostDto, Post>();

            Mapper.CreateMap<FAQ, FAQDto>();
            Mapper.CreateMap<FAQDto, FAQ>();
        }
    }
}