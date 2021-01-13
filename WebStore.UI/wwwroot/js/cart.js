var addOneToCart = function (e) {
    console.log(e);
    var stockId = e.target.dataset.stockId;
    addToCart(stockId, 1)
}
var minusOneFromCart = function (e) {
    console.log(e);
    var stockId = e.target.dataset.stockId;
    removeFromCart(stockId, 1);
}
var addToCart = function (stockId, qty) {
    axios.post('/Cart/AddOne/' + stockId + '/' + qty)
        .then(res => {
            updateCart();
        })
        .catch(err => {
            alert(err.error);
        });
}
var removeAllFromCart = function (e) {
    console.log(e);
    var stockId = e.target.dataset.stockId;
    var id = 'stock-qty-' + stockId;
    var el = document.getElementById(id);
    var qty = parseInt(el.innerText);
    removeFromCart(stockId, qty);

}
var removeFromCart = function (stockId, qty) {
    axios.post('/Cart/Remove/' + stockId + '/' + qty)
        .then(res => {
            updateCart();
        })
        .catch(err => {
            alert(err.error);
        });
}
var updateCart = function () {
    axios.get('/Cart/GetCartNav')
        .then(res => {
            var html = res.data;
            var el = document.getElementById('nav-cart');
            el.outerHTML = html;
        }).
        catch(err => {
            alert(err.error);
        });

    axios.get('/Cart/GetCartMain')
        .then(res => {
            var html = res.data;
            var el = document.getElementById('main-cart');
            el.outerHTML = html;
        }).
        catch(err => {
            alert(err.error);
        });
}