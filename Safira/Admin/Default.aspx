<%@ page title="" language="C#" masterpagefile="~/Admin/MasterPages/MasterPage.master" autoeventwireup="true" codefile="Default.aspx.cs" inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="content">

        <div class="breadLine">
            <ul class="breadcrumb">
                <!--Nút ẩn/hiện menu bên góc trái-->
                <li>
                    <a href="#" title="Ẩn thanh menu" class="sidebarControl menu-arrow">
                        <span class="icon-arrow-left"></span>
                    </a>
                </li>
                <!--Thanh breadcrumb-->
                <li>
                    <a href="#">Bàn Làm Việc</a>
                </li>
            </ul>
        </div>

        <div class="workplace">
            <div class="page-header">
                <h1>Chào mừng đến với chương trình quản lý website.
                            <br />
                    Hãy chọn Menu để đến trang chức năng
                </h1>
            </div>

            <div class="row-fluid">
                <div class="span12">
                    <div class="head clearfix">
                        <h1>Thống Kê Đơn Hàng Năm 2022</h1>
                    </div>
                    <div class="block-fluid table-sorting clearfix">
                        <!--Filter-->
                        <div class="dataTables_filter">
                        </div>

                        <div class="dataTables_length">
                        </div>
                        <!--Content-->
                        <table cellpadding="0" cellspacing="0" width="100%" class="table listAccounts">
                            <tbody>
                                <div>
                                    <canvas id="myChart"></canvas>
                                </div>
                            </tbody>
                        </table>
                        <!--Pagging-->

                    </div>
                </div>
            </div>
        </div>



        <script>
            var xValues = ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5"];

            new Chart("myChart", {
                type: "line",
                data: {
                    labels: xValues,
                    datasets: [{
                        label: 'Thanh Toán',
                        data: [860, 1140, 1060, 1060, 1110, 1330, 2210, 7830, 2478],
                        borderColor: "#23883a",
                        fill: false
                    }, {
                            label: 'Giao Hàng',
                            data: [1600, 1700, 1700, 1900, 2000, 2700, 4000, 5000, 6000, 7000],
                            borderColor: "#ffc107",
                            fill: false
                        }, {
                            label: 'Đặt Hàng',
                            data: [300, 700, 2000, 5000, 6000, 4000, 2000, 1000, 200, 100],
                            borderColor: "#17a2b8",
                            fill: false
                        },
                        {
                             
                            label: 'Sản Phẩm',
                            data: [300, 700, 2000, 3000, 9000, 4000, 2000, 1000, 200, 200],
                            borderColor: "#ccc",
                            fill: false
                        
                        }

                    ]
                },
                
            });
        </script>
    </div>
</asp:Content>

