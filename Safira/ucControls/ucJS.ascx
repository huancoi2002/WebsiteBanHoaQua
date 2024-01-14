<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucJS.ascx.cs" Inherits="ucControls_ucJS" %>


<script src="assets/js/vendor/modernizr-3.7.1.min.js"></script>
<script src="assets/js/vendor/jquery-3.4.1.min.js"></script>
<script src="assets/js/bootstrap.min.js"></script>
<script src="../assets/js/jquery-ui.js"></script>
<script src="assets/js/jquery-search.js"></script>
<!--popper min js-->
<script src="assets/js/popper.js"></script>
<!--bootstrap min js-->
<!--modernizr min js here-->

<!--owl carousel min js-->
<script src="assets/js/owl.carousel.min.js"></script>
<!--slick min js-->
<script src="assets/js/slick.min.js"></script>
<!--magnific popup min js-->
<script src="assets/js/jquery.magnific-popup.min.js"></script>
<!--counterup min js-->
<script src="assets/js/jquery.counterup.min.js"></script>
<!--jquery countdown min js-->
<script src="assets/js/jquery.countdown.js"></script>
<!--jquery ui min js-->
<script src="assets/js/jquery.ui.js"></script>
<!--jquery elevatezoom min js-->
<script src="assets/js/jquery.elevatezoom.js"></script>
<!--isotope packaged min js-->
<script src="assets/js/isotope.pkgd.min.js"></script>
<!--slinky menu js-->
<script src="assets/js/slinky.menu.js"></script>
<!--instagramfeed menu js-->
<script src="assets/js/jquery.instagramFeed.min.js"></script>
<!-- tippy bundle umd js -->
<script src="assets/js/tippy-bundle.umd.js"></script>
<!-- Plugins JS -->
<script src="assets/js/plugins.js"></script>

<!-- Main JS -->
<script src="assets/js/main.js"></script>
<script>
    $(function ($) {
        InitAutoSearch(".search-query", "/ServiceUtility.asmx/SearchProduct", "/ProductGrid.aspx?keyword={0}");
    });

</script>
