using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace LR1.Controllers
{
    [Route("api/[controller]")] // Путь к контроллеру
    public class CommandsController : Controller // Наслудемся от контроллера
    {

        private readonly ICommandsRepository _repository;

        public CommandsController(ICommandsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet] //Метод будет реагировать на метод GET по пути /api/commands
        public IEnumerable<CommandEntity> Get()
        {
            return _repository.GetAllCommands(); //Выдает все команды
        }

        [HttpGet("find")] //Метод будет реагировать на метод GET по пути /api/commands/find?sourceName=что-то
        public IEnumerable<CommandEntity> Get(String sourceName)
        {
            return _repository.FindByCommandBySourceName(sourceName); //Ищем по названию источника
        }

        [HttpGet("{id}")] //Метод будет реагировать на метод GET по пути /api/commands/{id}, где {id} - идентификатор из базы данных (/api/commands/1, /api/commands/2, /api/commands/10 и тп)
        public CommandEntity Get(int id)
        {
            return _repository.GetCommandById(id); //Ищем в БД по ИД и возвращаем
        }

        [HttpPost] //Метод будет реагировать на метод POST по пути /api/commands
        public void Post([FromBody] CommandEntity value) // С помощью [FromBody] сущность достанется из Body запроса (JSON)
        {
            _repository.SaveCommand(value); //Сохраняем сущность
        }

        [HttpPut("{id}")] //Метод будет реагировать на метод PUT по пути /api/commands/{id}, где {id} - идентификатор из базы данных (/api/commands/1, /api/commands/2, /api/commands/10 и тп)
        public void Put(int id, [FromBody] CommandEntity value) // С помощью [FromBody] сущность достанется из Body запроса (JSON)
        {
            value.CommandId = id; //Заменяем ИД в БД на пришедший нам с пути
            _repository.UpdateCommandEntity(value); // Обновляем сущность в БД
        }

        [HttpDelete("{id}")] //Метод будет реагировать на метод DELETE по пути /api/commands/{id}, где {id} - идентификатор из базы данных (/api/commands/1, /api/commands/2, /api/commands/10 и тп)
        public void Delete(int id)
        {
            _repository.DeleteCommandEntity(id); // Удаляем сущност из БД
        }
    }
}

