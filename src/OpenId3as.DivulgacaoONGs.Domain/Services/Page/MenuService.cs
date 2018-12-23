using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Page
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public void Add(Menu menu)
        {
            _menuRepository.Add(menu);
        }

        public void AddRange(IEnumerable<Menu> menu)
        {
            _menuRepository.AddRange(menu);
        }

        public void Delete(long id)
        {
            _menuRepository.Delete(id);
        }

        public void Dispose()
        {
            _menuRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Menu> GetAll()
        {
            return _menuRepository.GetAll();
        }

        public Menu GetById(long id)
        {
            return _menuRepository.GetById(id);
        }

        public void Update(Menu menu, long id)
        {
            _menuRepository.Update(menu, id);
        }
    }
}
