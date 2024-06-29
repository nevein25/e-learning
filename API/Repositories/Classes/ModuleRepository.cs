using API.Context;
using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repositories.Classes
{
    internal class ModuleRepository : IModuleRepositry
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;


        public ModuleRepository(EcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Add(Module module)
        {
            try
            {
                _context.Modules.Add(module);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Module>> Find(Expression<Func<Module, bool>> predicate) => await _context.Modules.Where(predicate).ToListAsync();

        public async Task<Module?> FindFirst(Expression<Func<Module, bool>> predicate) => await _context.Modules.Include(m => m.Course).FirstOrDefaultAsync(predicate);


        public async Task<bool> IfExist(Expression<Func<Module, bool>> predicate)=> await _context.Modules.AnyAsync(predicate);

        public Module MapToModule<T>(T moduleDto) where T : class => _mapper.Map<Module>(moduleDto);
       
    }
}