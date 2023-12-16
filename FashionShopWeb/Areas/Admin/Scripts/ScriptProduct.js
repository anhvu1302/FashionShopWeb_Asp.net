$(document).ready(function () {

    let closeBtnAddPro = document.querySelector('.add-product .close-btn');
    let addProduct = document.querySelector('.add-product');

    closeBtnAddPro.addEventListener('click', function () {
        addProduct.classList.remove('active');
    });
    let formAddProduct = document.getElementById('btn-add-product');
    formAddProduct.addEventListener('click', function () {
        addProduct.classList.add('active');
    });

    //Render loại sản phẩm khi chọn loại sản phẩm cha
    $("#parentProductTypeDropdown").change(function () {
        var selectedParentTypeId = $(this).val();
        if (selectedParentTypeId === "") {
            $("#clothes-style").empty();
            $("#clothes-style").append('<option value="">Select an option</option>');
        } else {
            $.ajax({
                url: "/Categories/GetLoaiSanPham",
                type: "GET",
                data: { parentId: selectedParentTypeId },
                success: function (data) {
                    $("#clothes-style").empty();

                    $("#clothes-style").append('<option value="">Select an option</option>');

                    $.each(data, function (index, item) {
                        $("#clothes-style").append('<option value="' + item.Value + '">' + item.Text + '</option>');
                    });
                },
                error: function (error) {
                    console.log("Error: " + error);
                }
            });
        }
    });



    // Thêm sản phẩm
    $('#add-product-form').submit(function (e) {
        e.preventDefault();

        var formData = $(this).serialize();

        // Make an AJAX request
        $.ajax({
            type: 'POST',
            url: '/Products/Add',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert('Thêm sản phầm thành công!');
                    $('#add-product-form')[0].reset();
                    addProduct.classList.remove('active');
                } else {
                    alert('Error: ' + response.message);
                }
            },
            error: function (error) {
                console.log('Error: ', error);
            }
        });
    });


    //  Xóa sản phẩm
    $('.delete-product-btn').on('click', function () {
        var productId = $(this).data('product-id');

        // Make an AJAX request to delete the product
        $.ajax({
            type: 'POST',
            url: '/Products/Delete',
            data: { idSanPham: productId },
            success: function (response) {
                if (response.success) {
                    alert('Xóa sản phẩm thành công!');                                     
                    window.location.reload();
                } else {
                    alert('Error: ' + response.message);
                }
            },
            error: function (error) {
                console.log('Error: ', error);
            }
        });
    });


    let closeBtnEditPro = document.querySelector('.edit-product .close-btn');
    let editProduct = document.querySelector('.edit-product');

    closeBtnEditPro.addEventListener('click', function () {
        editProduct.classList.remove('active');
    });
    $("#editParentProductTypeDropdown").change(function () {
        refreshClothesStyleDropdown($(this).val()).then(function () {
        });
    });

    $('.edit-product-btn').click(function () {
        var productId = $(this).data('product-id');

        $.ajax({
            url: '/Products/GetDetails',
            method: 'GET',
            data: { productId: productId },
            success: function (data) {
                $('#edit-product-id-header').text('ID: ' + data.productData.IdSanPham);
                $('#edit-product-id').val(data.productData.IdSanPham);
                $('#edit-product-name').val(data.productData.TenSanPham);
                var selectedParentTypeId = data.idLoaiCha;
                $('#editParentProductTypeDropdown').val(selectedParentTypeId);
                refreshClothesStyleDropdown(selectedParentTypeId, function () {
                    $('#edit-clothes-style').val(data.productData.IdLoaiSP);
                });
                $('#edit-product-price').val(data.productData.GiaBan);
                $('#edit-product-discount').val(data.productData.GiamGia);
                $('#edit-product-status').val(data.productData.TonTai.toString());
                $('#preview-image').attr('src', '/Content/Images/' + data.productData.AnhSP);
                $('#preview-image-compact-1').attr('src', '/Content/Images/' + data.productData.AnhSPChiTiet1);
                $('#preview-image-compact-2').attr('src', '/Content/Images/' + data.productData.AnhSPChiTiet2);
                $('#edit-product-content').text(data.productData.NoiDungSanPham);
                $('#edit-product-review').text(data.productData.DanhGiaSanPham);
                $('#edit-product-payment-shipping').text(data.productData.ThanhToanVanChuyen)
                document.querySelector('.edit-product').classList.add('active');
            },
            error: function (error) {
                console.error('Error fetching product details:', error);
            }
        });
    });
    $('#edit-image-url').change(function (event) {
        if (event.target.files.length > 0) {
            var imageUrl = URL.createObjectURL(event.target.files[0]);
            $('#preview-image').attr('src', imageUrl);
        } else {
            console.log('Không có tệp nào được chọn');
        }
    });
    $('#edit-image-url-compact-1').change(function (event) {
        if (event.target.files.length > 0) {
            var imageUrl = URL.createObjectURL(event.target.files[0]);
            $('#preview-image-compact-1').attr('src', imageUrl);
        } else {
            console.log('Không có tệp nào được chọn');
        }
    });
    $('#edit-image-url-compact-2').change(function (event) {
        if (event.target.files.length > 0) {
            var imageUrl = URL.createObjectURL(event.target.files[0]);
            $('#preview-image-compact-2').attr('src', imageUrl);
        } else {
            console.log('Không có tệp nào được chọn');
        }
    });
    function refreshClothesStyleDropdown(selectedParentTypeId, callback) {
        if (selectedParentTypeId === "") {
            $("#edit-clothes-style").empty();
            $("#edit-clothes-style").append('<option value="">Select an option</option>');
            if (callback) {
                callback();
            }
        } else {
            $.ajax({
                url: "/Categories/GetLoaiSanPham",
                type: "GET",
                data: { parentId: selectedParentTypeId },
                success: function (data) {
                    $("#edit-clothes-style").empty();
                    $("#edit-clothes-style").append('<option value="">Select an option</option>');

                    $.each(data, function (index, item) {
                        $("#edit-clothes-style").append('<option value="' + item.Value + '">' + item.Text + '</option>');
                    });

                    if (callback) {
                        callback();
                    }
                },
                error: function (error) {
                    console.log("Error: " + error);
                }
            });
        }
    }

    //Cập nhật sản phẩm
    $('#edit-product-form').submit(function (e) {
        e.preventDefault();

        var confirmation = confirm("Bạn có chắc muốn cập nhật thông tin?");

        if (confirmation) {
            $('#image-source').val(getBaseFilename($('#preview-image').attr('src')));
            $('#image-compact-source-1').val(getBaseFilename($('#preview-image-compact-1').attr('src')));
            $('#image-compact-source-2').val(getBaseFilename($('#preview-image-compact-2').attr('src')));

            var formData = new FormData($(this)[0]);
            console.log();

            $.ajax({
                url: '/Products/Update',
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.success) {
                        alert('Cập nhật thông tin thành công!');
                        document.querySelector('.edit-product').classList.remove('active');
                        location.reload();
                    } else {
                        alert('Update failed. ' + data.message);
                    }
                },
                error: function (error) {
                    console.log('Error: ' + error);
                }
            });
        } else {
            alert('Update cancelled.');
        }
    });

    function getBaseFilename(filename) {
        return filename.replace(/^.*[\\\/]/, '');
    }
});

