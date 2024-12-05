
using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Authors.Queries.GetAll
{
    public class GetAllAuthorQueryHandler : IRequestHandler<GetAllAuthorQuery, List<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAllAuthorQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Author>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
        {
            return await _authorRepository.GetAllAuthors();
        }

    }
}
