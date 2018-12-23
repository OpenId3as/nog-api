using AutoMapper;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenId3as.DivulgacaoONGs.Application.Services.Page
{
    public class MenuAppService : IMenuAppService
    {
        private readonly IMapper _mapper;
        private readonly IMenuService _menuService;
        private readonly IUnitOfWork _uow;

        public MenuAppService(IMapper mapper, IMenuService menuService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _menuService = menuService;
            _uow = uow;
        }

        public MenuViewModel Add(MenuViewModel menuViewModel)
        {
            var menu = _mapper.Map<MenuViewModel, Menu>(menuViewModel);
            _menuService.Add(menu);
            return menuViewModel;
        }

        public void Delete(long id)
        {
            _menuService.Delete(id);
        }

        public void Dispose()
        {
            _menuService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<MenuViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<Menu>, IEnumerable<MenuViewModel>>(_menuService.GetAll());
        }

        public MenuViewModel GetById(long id)
        {
            return _mapper.Map<Menu, MenuViewModel>(_menuService.GetById(id));
        }

        public MenuViewModel Update(MenuViewModel menuViewModel)
        {
            var menu = _mapper.Map<MenuViewModel, Menu>(menuViewModel);
            _menuService.Update(menu, menu.Id);
            return menuViewModel;
        }
    }
}
