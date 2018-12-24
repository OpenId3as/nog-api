using AutoMapper;
using OpenId3as.DivulgacaoONGs.Application.DataTransferObject.PagedSearch;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services;
using OpenId3as.DivulgacaoONGs.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Application.Services.Collaborators
{
    public class CollaboratorAppService : BaseAppService, ICollaboratorAppService
    {
        private readonly IMapper _mapper;
        private readonly ICollaboratorService _collaboratorService;
        private readonly IUnitOfWork _uow;

        public CollaboratorAppService(IMapper mapper, ICollaboratorService collaboratorService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _collaboratorService = collaboratorService;
            _uow = uow;
        }

        public CollaboratorViewModel Add(CollaboratorViewModel collaboratorViewModel)
        {
            var collaborator = _mapper.Map<CollaboratorViewModel, Collaborator>(collaboratorViewModel);

            _uow.BeginTransaction();
            var collaboratorReturn = _collaboratorService.Add(collaborator);
            _uow.Commit();

            collaboratorViewModel = _mapper.Map<Collaborator, CollaboratorViewModel>(collaboratorReturn);
            return collaboratorViewModel;
        }

        public void Delete(long id)
        {
            _uow.BeginTransaction();
            _collaboratorService.Delete(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _collaboratorService.Dispose();
            GC.SuppressFinalize(this);
        }

        public PagedSearch<CollaboratorViewModel> FindWithPagedSearch(List<SortItemDTO> sort, int limitRows = 50, int page = 0, string firstName = "", string lastName = "", string email = "", bool? active = null)
        {
            page = page > 0 ? page - 1 : 0;

            var sortableColumns = new Dictionary<string, string>();
            sortableColumns.Add("FirstName", "st_first_name");
            sortableColumns.Add("LastName", "st_last_name");
            sortableColumns.Add("Email", "st_email");
            sortableColumns.Add("Active", "bo_active");

            limitRows = this.ValidateLimitRows(limitRows);
            page = this.ValidatePageNumber(page);
            sort = this.ValidateSort(sortableColumns, sort);

            var filters = new Dictionary<string, object>();
            filters.Add("FirstName", firstName);
            filters.Add("LastName", lastName);
            filters.Add("Email", email);
            filters.Add("Active", active);

            var sortConcat = sort.Count > 0 ? sort.Select(x => string.Format("{0} {1}", x.Field, x.Direction)).Aggregate((current, next) => current + ", " + next) : string.Empty;
            var directionsConcat = sort.Count > 0 ? sort.Select(x => x.Direction).Aggregate((current, next) => current + ", " + next) : string.Empty;
            var fieldsConcat = sort.Count > 0 ? sort.Select(x => x.Field).Aggregate((current, next) => current + ", " + next) : string.Empty;

            var collaboratorReturn = _collaboratorService.FindWithPagedSearch(sortConcat, limitRows, page, firstName, lastName, email, active);
            var totalResults = _collaboratorService.GetCountPagedSearch(firstName, lastName, email, active);

            return new PagedSearch<CollaboratorViewModel>
            {
                CurrentPage = page + 1,
                PageSize = limitRows,
                TotalResults = totalResults,
                SortDirections = directionsConcat,
                SortFields = fieldsConcat,
                Filters = filters,
                List = _mapper.Map<IEnumerable<Collaborator>, IEnumerable<CollaboratorViewModel>>(collaboratorReturn).ToList()
            };
        }

        public IEnumerable<CollaboratorViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<Collaborator>, IEnumerable<CollaboratorViewModel>>(_collaboratorService.GetAll());
        }

        public CollaboratorViewModel GetById(long id)
        {
            return _mapper.Map<Collaborator, CollaboratorViewModel>(_collaboratorService.GetById(id));
        }

        public CollaboratorViewModel Update(CollaboratorViewModel collaboratorViewModel)
        {
            _uow.BeginTransaction();
            _collaboratorService.Update(_mapper.Map<CollaboratorViewModel, Collaborator>(collaboratorViewModel));
            _uow.Commit();
            return collaboratorViewModel;
        }
    }
}