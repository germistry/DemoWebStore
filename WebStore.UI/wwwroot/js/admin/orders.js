﻿var app = new Vue({
    el: '#app',
    data: {
        status: 0,
        loading: false,
        orders: [],
        selectedOrder: null
    },
    mounted() {
        this.getOrders();
    },
    watch: {
        status: function () {
            this.getOrders();
        }
    },
    methods: {
        getOrders() {
            this.loading = true;
            axios.get('/orders?status=' + this.status)
                .then(res => {
                    console.log(res.data);
                    this.orders = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        selectOrder(id) {
            this.loading = true;
            axios.get('/orders/' + id)
                .then(res => {
                    console.log(res);
                    this.selectedOrder = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        updateOrder() {
            this.loading = true;
            axios.put('/orders/' + this.selectedOrder.id, null)
                .then(res => {
                    console.log(res.data);
                    this.loading = false;
                    this.exitOrder();
                    this.getOrders();
                })
                .catch(err => {
                    console.log(err);
                });
        },
        exitOrder() {
            this.selectedOrder = null;
        },
        
    },
    computed: {
    }
})
