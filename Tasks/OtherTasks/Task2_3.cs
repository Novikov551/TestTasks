namespace TestTasks1_51
{
    internal class Task2_3
    {
        public void Do()
        {
            /*
              2.3	Как выбрать данные из поля с XML?
                Написать запрос, выбирающий данные из XML из предыдущего вопроса
                Отфильтровать данные по StatusId != 3
             */

            var selectStr = "SELECT (xpath('//id/text()', node))[1]::text::int AS id," +
                "(xpath('//code/text()', node))[1]::text AS code," +
                "(xpath('//name/text()', node))[1]::text AS name," +
                "(xpath('//statusid/text()', node))[1]::text::int AS statusid" +
                "FROM unnest(xpath('/table/row','<table><row><id>1</id><code>gargadgadfga</code><name>Запрос предложений 1</name><statusid>45</statusid></row>" +
                "<row><id>2</id><code>bsftrggdfgadfgdfat</code><name>Запрос предложений 2</name><statusid>2</statusid></row>" +
                "<row><id>3</id><code>gfadgdfsgdfsgs</code><name>Запрос предложений 3</name><statusid>45</statusid></row>" +
                "<row><id>4</id><code>afgereaerffdgvdf</code><name>Запрос предложений 4</name><statusid>3</statusid></row>" +
                "<row><id>5</id><code>dgadfterdsgsdgad</code><name>Запрос предложений 5</name><statusid>45</statusid></row>" +
                "<row><id>6</id><code>argrgag</code><name>Запрос предложений 6</name><statusid>2</statusid></row></table>'::xml)) AS node" +
                "WHERE (xpath('//statusid/text()', node))[1]::text::int != 3;";
        }
    }
}
