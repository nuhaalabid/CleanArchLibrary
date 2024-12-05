//using Infrastructure.Database;
//using MediatR;
//using Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Applikation.Users.Queries.GetAll
//{
//    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUserQuery, List<User>>
//    {
//        private readonly FakeDatabase _database;

//        public GetAllUsersQueryHandler(FakeDatabase database)
//        {
//            _database = database;
//        }

//        public Task<List<User>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
//        {
//            return Task.FromResult(_database.Users);
//        }
//    }
//}

