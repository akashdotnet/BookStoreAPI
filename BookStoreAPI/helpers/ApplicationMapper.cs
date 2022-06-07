using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Models;

namespace BookStoreAPI.helpers
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Books,BookModel>().ReverseMap();
        }
    }
}
