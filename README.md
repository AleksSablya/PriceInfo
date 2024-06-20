# PriceInfo

PriceInfo.API has two endpoins

# api/assets
Gets list of supported market assets.

# api/price/{assetslist}

Gets price information for specific asset(s) separated by comma. E.g. api/price/EURUSD,EURGBP,GBPAUD


# To run API via Docker

збираємо image

<code>docker compose up</code>

переглянути створенні images

<code>docker images</code>

беремо image id для priceinfoapi и вставляємо замість <image id>

<code>docker run -p 8080:8080 <image id></code>

in browser try http://localhost:8080/api/assets

# Пояснення
В розробци була задіяна InMemoryDatabase щоб не використовувати реальну БД з міграціями.
В базі данних всього одна таблиця Assets куди заносяться данні з запиту до Fintatechs API api/instruments/v1/instruments. Ця діє виконується у BackgroundService після завантаження API.
Збереженны данні з таблиці відображаюсться точкою api/assets.
Для отримання данних по цінах вікористовується запит до Web-Socket API. Ці данні в БД не зберігаються. Якщо буде потреба, то можу доробити зберігання до бази данних.
