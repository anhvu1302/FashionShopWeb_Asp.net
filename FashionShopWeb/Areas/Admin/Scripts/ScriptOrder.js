$(document).ready(function () {   
    $(document).ready(function () {
        $('.confirmOrderBtn').click(function () {
            var orderId = $(this).data('order-id');
                $.ajax({
                    url: '/Orders/ConfirmOrder',
                    method: 'POST',
                    data: { orderId: orderId },
                    success: function (data) {
                        if (data.success) {
                            alert('Xác nhận đơn hàng thành công!');
                            window.location.reload();
                        } else {
                            alert('Error confirming order: ' + data.message);
                        }
                    },
                    error: function (error) {
                        console.error('Error confirming order:', error);
                    }
                });
        });
    });
});

