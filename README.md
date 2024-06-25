# PriceInfo

PriceInfo.API has two endpoins

# api/assets
Gets list of supported market assets.

# api/price/{assetslist}

Gets price information for specific asset(s) separated by comma. E.g. api/price/EURUSD,EURGBP,GBPAUD


# To run API via Docker

create image

<code>docker compose up</code>

view created images

<code>docker images</code>

take image id for priceinfoapi and put it instead of &lt;image id&gt;

<code>docker run -p 8080:8080 &lt;image id&gt;</code>

in browser try http://localhost:8080/api/assets

