//low stock or no stock functions
var toggleStock = function (e) {
    console.log(e);
    var stockToHide = document.querySelectorAll('.low-stock');
    stockToHide.forEach(function (e) {
        e.classList.add('is-hidden');
    });
    showLowStock(e.target.value);

}
var showLowStock = function (id) {
    var stockToShow = document.getElementById('low-stock-' + id);
    if (stockToShow !== null && stockToShow !== undefined) {
        stockToShow.classList.remove('is-hidden');
    }
}
showLowStock(document.getElementById('CartViewModel_StockId').value);