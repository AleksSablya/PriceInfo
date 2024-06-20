# PriceInfo

PriceInfo.API has two endpoins

# api/assets
Gets list of supported market assets.

# api/price/{assetslist}

Gets price information for specific asset(s) separated by comma. E.g. api/price/EURUSD,EURGBP,GBPAUD


To run API from docker
збираємо image
> docker compose up
переглянути створенні images
> docker images
беремо image id для priceinfoapi и вставляємо замість <image id>
> docker run -p 8080:8080 <image id>

in browser try http://localhost:8080/api/assets

Пояснення. 
В розробци була задіяна InMemoryDatabase щоб не використовувати реальну БД з міграціями.
В базі данних всього одна таблиця Assets куди заносяться данні з запиту до Fintatechs API api/instruments/v1/instruments. Ці данні з таблиці відображаюсться точкою api/assets
Для отримання данних по цінах вікористовуеться запит до Web-Socket API. Ці данні я в БД не зберігаю. Якщо буде треба, то можу доробити зберігання до бази данних.