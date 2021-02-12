var app = new Vue({
    el: '#app',
    data: {
        editing: false,
        loading: false,
        products: [],
        selectedProduct: null,
        usingProductMinValue: false,
        newStock: {
            productId: 0,
            stockName: "",
            qty: 0,
            stockValue: 0.00
        }
    },
    mounted() {
        this.getStock();
    },
    methods: {
        getStock() {
            this.loading = true;
            axios.get('/stocks')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        updateStock() {
            this.loading = true;
            axios.put('/stocks', {
                stock: this.selectedProduct.stock.map(x => {
                    return {
                        id: x.id,
                        stockName: x.stockName,
                        qty: x.qty,
                        stockValue: x.stockValue,
                        productId: this.selectedProduct.id
                    };
                })       
            })
                .then(res => {
                    console.log(res);
                    this.selectedProduct.stock.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false; 
                    this.editing = false;
                    this.selectedProduct = null;
                });
        },
        deleteStock(id, index) {
            this.loading = true;
            axios.delete('/stocks/' + id)
                .then(res => {
                    console.log(res);
                    this.selectedProduct.stock.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        addStock() {
            this.loading = true;
            axios.post('/stocks', this.newStock)
                .then(res => {
                    console.log(res);
                    this.selectedProduct.stock.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

        },
        selectProduct(product) {
            this.editing = true;
            this.selectedProduct = product;
            this.newStock.productId = product.id;
            this.usingProductMinValue = product.useProductMinValue;
            if (this.usingProductMinValue) {
                this.newStock.stockValue = product.minValue;
            }  
        },
        cancel() {
            this.editing = false;
            this.selectedProduct = null;
        }
    }
})