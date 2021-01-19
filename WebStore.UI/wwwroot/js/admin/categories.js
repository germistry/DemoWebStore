var app = new Vue({
    el: '#app',
    data: {
        editing: false,
        loading: false,
        objectIndex: 0,
        fileName: "",
        categoryModel: {
            id: 0,
            categoryName: "",
            description: "",
            ogTags: "",
            currentImage: "",
            image: ""
        },
        categories: []
    },
    mounted() {
        this.getCategories();
    },
    methods: {
        getCategory(id) {
            this.loading = true;
            axios.get('/categories/' + id)
                .then(res => {
                    console.log(res);
                    var category = res.data;
                    this.categoryModel = {
                        id: category.id,
                        categoryName: category.categoryName,
                        description: category.description,
                        ogTags: category.ogTags,
                        currentImage: category.currentImage
                    };
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        getCategories() {
            this.loading = true;
            axios.get('/categories')
                .then(res => {
                    console.log(res.data);
                    this.categories = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        createCategory() {
            this.loading = true;
            let formData = new FormData();
            formData.append('categoryName', this.categoryModel.categoryName)
            formData.append('description', this.categoryModel.description)
            formData.append('ogTags', this.categoryModel.ogTags)
            formData.append('currentImage', this.categoryModel.currentImage)
            formData.append('image', this.categoryModel.image)
            axios.post('/categories', formData)
                .then(res => {
                    console.log(res.data);
                    this.categories.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        updateCategory() {
            this.loading = true;
            let formData = new FormData();
            formData.append('id', this.categoryModel.id)
            formData.append('categoryName', this.categoryModel.categoryName)
            formData.append('description', this.categoryModel.description)
            formData.append('ogTags', this.categoryModel.ogTags)
            formData.append('currentImage', this.categoryModel.currentImage)
            formData.append('image', this.categoryModel.image)
            axios.put('/categories', formData)
                .then(res => {
                    console.log(res.data);
                    this.categories.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        deleteCategory(id, index) {
            this.loading = true;
            axios.delete('/categories/' + id)
                .then(res => {
                    console.log(res);
                    this.categories.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        newCategory() {
            this.editing = true;
            this.categoryModel.id = 0;
        },
        editCategory(id, index) {
            this.objectIndex = index;
            this.getCategory(id);
            this.editing = true;
        },
        handleFileUpload() {
            this.categoryModel.image = this.$refs.file.files[0];
            this.fileName = event.target.files[0].name;
        },
        cancel() {
            this.editing = false;
        }
    },
    computed: {
    }
})
