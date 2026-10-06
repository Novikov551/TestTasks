using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTasks1_51
{
    internal class Task2_2
    {
        /*2.2	Как вывести результат запроса в XML?
         
            Пусть есть таблица T со следующим видом и содержанием. Что вернет SQL запрос?

            Id	Code	            Name	                StatusId
            1	gargadgadfga	    Запрос предложений 1	45
            2	bsftrggdfgadfgdfat	Запрос предложений 2	2
            3	gfadgdfsgdfsgs	    Запрос предложений 3	45
            4	afgereaerffdgvdf	Запрос предложений 4	3
            5	dgadfterdsgsdgad	Запрос предложений 5	45
            6	argrgag	            Запрос предложений 6	2

            Написать запрос, выводящий данные в XML*/
    
        public void Do()
        {
            var createTableStr = "CREATE TABLE T (id SERIAL PRIMARY KEY," +
                "code TEXT," +
                "name TEXT," +
                "statusid INT);";

            var insertTableStr = "INSERT INTO T (id, code, name, statusid) " +
                "VALUES (1, 'gargadgadfga', 'Запрос предложений 1', 45)," +
                "(2, 'bsftrggdfgadfgdfat', 'Запрос предложений 2', 2)," +
                "(3, 'gfadgdfsgdfsgs', 'Запрос предложений 3', 45)," +
                "(4, 'afgereaerffdgvdf', 'Запрос предложений 4', 3)," +
                "(5, 'dgadfterdsgsdgad', 'Запрос предложений 5', 45)," +
                "(6, 'argrgag',            'Запрос предложений 6', 2);";

            //1й Вариант
            var selectXElementStr = "SELECT id, xmlelement(NAME \"Row\"," +
                "xmlforest(id AS \"Id\", code AS \"Code\"," +
                "name AS \"Name\"," +
                "statusid AS \"StatusId\")) AS xml_data" +
                "FROM t;";

            //2й 
            var selectTableStr = "SELECT table_to_xml('t', false, false, '');";
        }
    }
}
