<script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.min.js"></script>
    
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js"></script>
<script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js"></script>
<link rel="Stylesheet" type="text/css" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" />
        <script type="text/javascript">
            $(function () {
                $('#TextBox1').focus();
                var $inp = $('.cls');
                $inp.bind('keydown', function (e) {
                    //var key = (e.keyCode ? e.keyCode : e.charCode);
                    var key = e.which;
                    if (key == 13) {
                        e.preventDefault();
                        var nxtIdx = $inp.index(this) + 1;
                        $(".cls:eq(" + nxtIdx + ")").focus();
                        if (nxtIdx % 14 == 0) {
                            $(".cls:eq(" + nxtIdx + ")").focus();
                            $(".cls:eq(" + nxtIdx + ")").click();
                        }
                    }
                    if (key == 38) {
                        e.preventDefault();
                        var nxtIdx = $inp.index(this)-14;
                        $(".cls:eq(" + nxtIdx + ")").focus();
                    }
                    if (key == 40) {
                        e.preventDefault();
                        var nxtIdx = $inp.index(this) + 14;
                        $(".cls:eq(" + nxtIdx + ")").focus();
                    }
                    if (key == 37) {
                        e.preventDefault();
                        var nxtIdx = $inp.index(this) -1;
                        $(".cls:eq(" + nxtIdx + ")").focus();
                    }
                    if (key == 39) {
                        e.preventDefault();
                        var nxtIdx = $inp.index(this) + 1;
                        $(".cls:eq(" + nxtIdx + ")").focus();
                    }
                });
            });
    </script>
