namespace TestTasks1_51
{
    internal class Task4_1
    {
        public void Do()
        {
            /*
            4.1	Задача на валидность XML
                Перед вами пример XML файла, но он не пройдет валидацию. Найдите все ошибки.

            <PurchaseInfo>
                <PurchaseId>7380554</PurchaseId>
                <PurchaseCode>SBR031-1910280001</PurchaseCode>
                <PurchaseName>Конкурс с ценой < 500 000 руб.</PurchaseName>
                <TypeInfo><TypeName>Конкурс</TypeInfo></TypeName>
            </PurchaseInfo>
            <BidInfo>
                <BidId>652245</BidId>
                <BidName>Право на заключение договора</BidName>
                <BidNo>1</BidNo>
                <BidPrice>2000000.00</BidPrice>
                <BidCurrency>Российский рубль
                <BidCurrencyName>57287</BidCurrencyName>
            </BidInfo>
            <RequestInfo>
                <BuId AccessByOrganization=1>20535</BuId>
                <RequestBuName>ИП Анар Ростовский</RequestBuName>
                <RequestCreateDate>28.10.2019 17:42</RequestCreateDate>
                <RequestId>157545</RequestId>
                <RequestINN>100000000004</RequestINN>
                <RequestNo>2<RequestNo>
            </RequestInfo>

            Ответ:
            1) В xml документе не может быть несколько корневых элементов, сейчас их 3: <PurchaseInfo></PurchaseInfo> | <BidInfo></BidInfo> | <RequestInfo></RequestInfo>. Нужно либо обернуть в какой нибудь 1 общий тег, или создать 3 отдельных xml документа 
            2) Символ '<' в строке  <PurchaseName>Конкурс с ценой < 500 000 руб.</PurchaseName> не допустим, он считается как начало тега, его нужно заменить на '&lt;'
            3) В строке <TypeInfo><TypeName>Конкурс</TypeInfo></TypeName> тег <TypeName> не закрыт, нужно так: <TypeInfo><TypeName>Конкурс</TypeName></TypeInfo>
            4) <BidCurrency> - не закрытый тег 
            5) Значение атрибута AccessByOrganization не взято в кавычки, должно быть AccessByOrganization="1"
            6) Тег <RequestNo> не закрывается и затем открывается еще 1. Нужно <RequestNo>2</RequestNo>
            */
        }
    }
}
