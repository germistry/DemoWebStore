var app = new Vue({
    el: '#app',
    data: {
        editing: false,
        loading: false,
        objectIndex: 0,
        productModel: {
            id: 0,
            name: "",
            description: "",
            minValue: 0.00,
            currentImage: "",
            image: ""
        },
        products: []
    },
    mounted() {
        this.getProducts();
    },
    methods: {
        getProduct(id) {
            this.loading = true;
            axios.get('/products/' + id)
                .then(res => {
                    console.log(res);
                    var product = res.data;
                    this.productModel = {
                        id: product.id,
                        name: product.name,
                        description: product.description,
                        minValue: product.minValue,
                        currentImage: product.currentImage
                    };
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        getProducts() {
            this.loading = true;
            axios.get('/products')
                .then(res => {
                    console.log(res.data);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        createProduct() {
            this.loading = true;
            let formData = new FormData();
            formData.append('name', this.productModel.name)
            formData.append('description', this.productModel.description)
            formData.append('minValue', this.productModel.minValue)
            formData.append('currentImage', this.productModel.currentImage)
            formData.append('image', this.productModel.image)
            axios.post('/products', formData)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        updateProduct() {
            this.loading = true;
            let formData = new FormData();
            formData.append('id', this.productModel.id)
            formData.append('name', this.productModel.name)
            formData.append('description', this.productModel.description)
            formData.append('minValue', this.productModel.minValue)
            formData.append('currentImage', this.productModel.currentImage)
            formData.append('image', this.productModel.image)
            axios.put('/products', formData)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete('/products/' + id)
                .then(res => {
                    console.log(res);
                    this.products.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        newProduct() {
            this.editing = true;
            this.productModel.id = 0;
        },
        editProduct(id, index) {
            this.objectIndex = index;
            this.getProduct(id);
            this.editing = true;
        },
        handleFileUpload() {
            this.productModel.image = this.$refs.file.files[0];
        },
        cancel() {
            this.editing = false;
        }
    },
    computed: {
    }
})
