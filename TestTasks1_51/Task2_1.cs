using System;

public class Task2_1
{
    /*
    2.1	Вопрос на знание select … join
    Есть две таблицы:
    Т1(ID int, Text1 …., text2 …. B и т.д.)
    И
    Т2(ID int, Text1 …., text2 …. B и т.д.)

    Написать select
    1.	Вывести все поля из обеих таблиц, вывести записи при условии, что ID обеих таблиц совпадают.
    2.	Вывести все поля из обеих таблиц, вывести все записи из T1 и только имеющиеся в T2
    3.	Вывести все записи из T1, при условии, что таких ID нет в T2
    */


    //Создаю таблицы
    var createFirstTableStr = "CREATE TABLE T1 (id SERIAL PRIMARY KEY," +
        "Text1 TEXT," +
        "Text2 TEXT)";

    var createSecondTableStr = "CREATE TABLE T2 (id SERIAL PRIMARY KEY," +
        "Text1 TEXT," +
        "Text2 TEXT)";

    //Заполняю их данными
    var insertFirstTableStr = "INSERT INTO T1 (id, Text1, Text2) " +
        "VALUES (1, 'Alpha',   'One')," +
        "(2, 'Beta',    'Two')," +
        "(3, 'Gamma',   'Three')," +
        "(4, 'Delta',   'Four')," +
        "(5, 'Epsilon', 'Five')";

    var insertSecondTableStr = "INSERT INTO T2 (id, Text1, Text2)" +
        "VALUES(1, 'Apple',   'Red')," +
        "(2, 'Banana',  'Yellow')," +
        "(3, 'Cherry',  'Dark'),\" +
        "(7, 'Fig',     'Purple')
        "(8, 'Grape',   'Green')";

        //1)
        var getBothTablesJoinedRowsByID = "SELECT tab1.*, tab2.*" +
        "FROM t1 tab1" +
        "JOIN t2 tab2 ON tab2.id = tab1.id";

        //2)
        var getBothTablesRowsLeftJoinFirstTable = "SELECT tab1.*, tab2.*" +
        "FROM t1 tab1" +
        "LEFT JOIN t2 tab2 ON tab2.id = tab1.id";

        //3)
        var getFirstTablesRowsWhereSecondTableDoesntHave = "SELECT tab1.*" +
        "FROM t1 tab1" +
        "LEFT JOIN t2 tab2 ON tab2.id = tab1.id" +
        "WHERE tab2.id IS NULL";
}
