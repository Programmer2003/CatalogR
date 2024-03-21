
let search = $('#search-input');
let result = $('#searchResult');
let loader = $('#loader');
let menu = $('#searchMenu');
let delay = null;
search.on("focus", function () {
    menu.show();
    clearTimeout(delay);
});
search.on("blur", function () {
    setTimeout(() => menu.hide(), 300);
});

let timer = null;
function restartTimer() {
    result.empty();
    loader.show();
    clearTimeout(timer);
    timer = setTimeout(updateSearchResult, 750);
}

search.on('input', restartTimer);

function updateSearchResult() {
    console.log('start');
    var value = $('#search-input').val();
    var data = { query: value };
    $.ajax({
        type: "POST",
        url: '/Search/ItemsSearch',
        data: data,
        success: function (data) {
            //console.log(Object.keys(data))
            //console.log(Object.values(data))
            //console.log(Object.getOwnPropertyNames(data));
            loader.hide();
            result.empty();
            console.log(data);
            if (!Array.isArray(data) || data.length == 0) {
                result.append('<div class="list-group-item list-group-item-action d-flex gap-3 py-3">No results</div>');
            }
            else {
                addItems(data, value);
            }
        },
    });
}

function addItems(data, query) {
    $(data).each(function () {
        let item = $(this)[0];
        let properties = item.fullTextIndexPropetries;
        console.log(properties);
        let founded = properties.find(x => x.toLowerCase().includes(query.toLowerCase()));
        if (!founded) founded = item.name;
        console.log(founded);
        let html =
            `<a href="Items/Details/${item.id}" class="list-group-item list-group-item-action d-flex gap-3 py-3" aria-current="true">
                <div class="d-flex gap-2 w-100 justify-content-between">
                <div>
                    <h6 class="mb-0">${item.name}</h6>
                    <p class="mb-0 opacity-75">${founded}</p>
                </div>
                </div>
            </a>`;
        result.append(html);
    });
}
