using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CosmosDBUtilitiesExample.Data;
using CosmosDBUtilitiesExample.DTOs;
using CosmosDBUtilitiesExample.Models.CosmosDB;
using MvcControlsToolkit.Business.DocumentDB;
using MvcControlsToolkit.Core.Business.Utilities;

namespace CosmosDBUtilitiesExample.Repository
{
    public class ToDoItemsRepository: DocumentDBCRUDRepository<ToDoItem>
    {
        private string loggedUser;
        public ToDoItemsRepository(
            IDocumentDBConnection connection,
            string userName
            ): base(connection, 
                CosmosDBDefinitions.ToDoItemsId,
                m => m.Owner == userName,
                m => m.Owner == userName
                )
        {
            loggedUser = userName;
        }
        static ToDoItemsRepository()
            {
            DeclareProjection
                (m =>
                new ListItemDTO
                {

                    
                }, m => m.Id
            );
            DeclareProjection
                (m =>
                new DetailItemDTO
                {

                    SubItems = m.SubItems
                        .Select(l => new SubItemDTO { })
                }, m => m.Id
            );
            DeclareUpdateProjection<DetailItemDTO>
               (m =>
                   new ToDoItem
                   {
                       SubItems = m.SubItems
                        .Select(l => new ToDoItem { }),
                       AssignedTo = m.AssignedToId == null ? 
                        null : new Person { }
                   }
               );
            DeclareProjection
                (m =>
                new SubItemDTO
                {


                }, m => m.Id
            );
           DocumentDBCRUDRepository<Person>
                .DeclareProjection
               (m =>
                new PersonListDTO
                {


                }, m => m.Surname
            );
        }
        public async Task<DataPage<ListItemDTO>> GetAllItems()
        {
            var vm = await GetPage<ListItemDTO>
                (null,
                 x => x.OrderBy(m => m.Name),
                 -1, 100);
            return vm;
        }
        public async Task<IList<SubItemDTO>> AllSubItems()
        {
            var query=Table(100)
                .Where(SelectFilter)
                .SelectMany(m => m.SubItems);
            return await ToList<SubItemDTO>(query);  
        }
        public async Task<IList<PersonListDTO>> AllMembers()
        {
            
            var query = 
            Table(100)
            .Where(SelectFilter)
            .SelectMany(m => m.Team);
            return await ToList<PersonListDTO, Person>(query);
        }
    }
}
