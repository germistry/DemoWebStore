﻿var app = new Vue({
    el: '#app',
    data: {
        editing: false,
        loading: false,
        productObjectIndex: 0,
        fileName: "",
        categoryList: [{
            value: 0,
            text: ""
        }],
        productModel: {
            id: 0,
            name: "",
            description: "",
            extendedDescription: "",
            ogTags: "",
            createdDate: "",
            currentUpdatedDate: "",
            useProductMinValue: true,
            minValue: 0.00,
            currentImage: "",
            image: "",
            categoryId: 0
        },
        products: []
        
    },
    mounted() {
        this.getProducts();
        this.getCategoryDropdown();
    },
    methods: {
        getCategoryDropdown() {
            this.loading = true;
            axios.get('/products/getcategories')
                .then(res => {
                    console.log(res);
                    this.categoryList = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
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
                        extendedDescription: product.extendedDescription,
                        ogTags: product.ogTags,
                        createdDate: product.createdDate,
                        currentUpdatedDate: product.currentUpdatedDate,
                        useProductMinValue: product.useProductMinValue,
                        minValue: product.minValue,
                        currentImage: product.currentImage,
                        categoryId: product.categoryId
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
            formData.append('extendedDescription', this.productModel.extendedDescription)
            formData.append('ogTags', this.productModel.ogTags)
            formData.append('useProductMinValue', this.productModel.useProductMinValue)
            formData.append('minValue', this.productModel.minValue)
            formData.append('currentImage', this.productModel.currentImage)
            formData.append('image', this.productModel.image)
            formData.append('categoryId', this.productModel.categoryId)
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
            formData.append('extendedDescription', this.productModel.extendedDescription)
            formData.append('ogTags', this.productModel.ogTags)
            formData.append('useProductMinValue', this.productModel.useProductMinValue)
            formData.append('minValue', this.productModel.minValue)
            formData.append('currentImage', this.productModel.currentImage)
            formData.append('image', this.productModel.image)
            formData.append('categoryId', this.productModel.categoryId)
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
            this.getCategoryDropdown();
            this.categoryList = this.getCategoryDropdown;
            this.productModel.categoryId = 0;
        },
        editProduct(id, index) {
            this.editing = true;
            this.productObjectIndex = index;
            this.getProduct(id);
            this.getCategoryDropdown();
            this.categoryList = this.getCategoryDropdown;
            
        },
        handleFileUpload() {
            this.productModel.image = this.$refs.file.files[0];
            this.fileName = event.target.files[0].name;
        },
        cancel() {
            this.editing = false;
        }
    },
    computed: {
    }
})
