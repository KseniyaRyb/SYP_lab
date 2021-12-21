using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LR1
{
    public class CommandEntity
    {

        [Key] //Следующее поле/свойство будет первичным ключом в таблице
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Способ автогенерации ключа
        public int CommandId { get; set; } // ИД команды
        public string? CommandTrigger { get; set; } // Слово, на которое команда будет вызываться
        public string? SourceNames { get; set; } // Список источников, откуда команда может приходить
        public string? CommandAnswer { get; set; } // Ответ команды
        public Boolean IsScript { get; set; } // Является ли скриптом
        public String? ScriptName { get; set; } // Название скрипта
        public DateTime Date { get; set; }
        public String? Description { get; set; }
        public String? Autor { get; set; }
    }
    
}
