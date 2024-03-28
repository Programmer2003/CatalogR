
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
    var value = $('#search-input').val();
    $.ajax({
        type: "POST",
        url: '/Search/FastSearch',
        data: { query: value },
        success: function (data) {
            loader.hide();
            result.empty();
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
        let founded = properties.find(x => x.toLowerCase().includes(query.toLowerCase()));
        if (!founded) founded = item.name;
        let html =
            `<a href="${item.url}/${item.id}" class="list-group-item list-group-item-action d-flex gap-3 py-3" aria-current="true">
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
