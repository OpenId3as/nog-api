using AutoMapper;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ViewModels;
using OpenId3as.DivulgacaoONGs.Domain.Entities;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services;
using OpenId3as.DivulgacaoONGs.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _uow;

        public UserAppService(IMapper mapper, IUserService userService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _userService = userService;
            _uow = uow;
        }

        public UserViewModel Add(UserViewModel userViewModel)
        {
            var user = _mapper.Map<UserViewModel, User>(userViewModel);

            _uow.BeginTransaction();
            var userReturn = _userService.Add(user);
            _uow.Commit();

            userViewModel = _mapper.Map<User, UserViewModel>(userReturn);
            return userViewModel;
        }

        public void Delete(long id)
        {
            _uow.BeginTransaction();
            _userService.Delete(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _userService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(_userService.GetAll());
        }

        public UserViewModel GetById(long id)
        {
            return _mapper.Map<User, UserViewModel>(_userService.GetById(id));
        }

        public UserViewModel Update(UserViewModel userViewModel)
        {
            _uow.BeginTransaction();
            _userService.Update(_mapper.Map<UserViewModel, User>(userViewModel));
            _uow.Commit();
            return userViewModel;
        }
    }
}
