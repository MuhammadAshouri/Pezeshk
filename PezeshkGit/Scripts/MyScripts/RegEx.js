function Validator(ele, str) {
    TrueOrFalse = true;
    let eleVal = $(ele).val().trim();

    // Contact Mode

    // FullName
    if (str === "fullname") {
        if (!/^([\u0600-\u06FF]|\s){6,30}$/.test(eleVal)) {
            $(ele).css({ "color": "red", "border-color": "red" });
            TrueOrFalse = false;
        }
        else $(ele).css({ "color": "#aaa", "border-color": "green" });
    }

    // Email
    if (str === "email1") {
        var reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!reg.test(eleVal)) {
            $(ele).css({ "color": "red", "border-color": "red" });
            TrueOrFalse = false;
        }

        else $(ele).css({ "color": "#aaa", "border-color": "green" });
    }

    // Phone
    if (str === "phone1") {
        if (!/^(09)\d{9}$/.test(eleVal)) {
            $(ele).css({ "color": "red", "border-color": "red" });
            TrueOrFalse = false;
        }
        else $(ele).css({ "color": "#aaa", "border-color": "green" });
    }

    // Title
    if (str === "title1") {
        if (!/^.{3,50}$/.test(eleVal)) {
            $(ele).css({ "color": "red", "border-color": "red" });
            TrueOrFalse = false;
        }
        else $(ele).css({ "color": "#aaa", "border-color": "green" });
    }

    // MessageText
    if (str === "messageText") {
        let remain = $(ele).next(".messageTextRemain").html(eleVal.length + "/400");
        if (eleVal.length > 400) {
            remain.css("color", "red");
            $(ele).css({ "color": "red", "border-color": "red" });
            TrueOrFalse = false;
        }
        else {
            $(ele).css({ "color": "#aaa", "border-color": "green" });
            remain.css("color", "#aaa");
        }
    }


    // Panel Mode

    // FirstName
    if (str === "firstname") {
        if (!/^([\u0600-\u06FF]|\s){3,30}$/.test(eleVal)) {
            $(ele).prev("label").html("نام: تنها حروف فارسی مجاز است.").css("color", "red");
            TrueOrFalse = false;
        }
        else $(ele).prev("label").html("نام:").css("color", "#43a047");
    }

    // LastName
    if (str === "lastname") {
        if (!/^([\u0600-\u06FF]|\s){3,30}$/.test(eleVal)) {
            $(ele).prev("label").html("نام خانوادگی: تنها حروف فارسی مجاز است.").css("color", "red");
            TrueOrFalse = false;
        }
        else $(ele).prev("label").html("نام خانوادگی:").css("color", "#43a047");
    }

    // Title
    if (str === "title") {
        if (!/^([\u0600-\u06FF]|\s){3,20}$/.test(eleVal)) {
            $(ele).prev("label").html("نام: تنها حروف فارسی مجاز است.").css("color", "red");
            TrueOrFalse = false;
        }
        else $(ele).prev("label").html("نام:").css("color", "#43a047");
    }

    // Title2
    if (str === "title2") {
        if (!/^.{5,100}$/.test(eleVal)) {
            $(ele).prev("label").html("عنوان: حداقل پنج حرف.").css("color", "red");
            TrueOrFalse = false;
        }
        else $(ele).prev("label").html("عنوان:").css("color", "#43a047");
    }

    // Description
    if (str === "des") {
        if (!/^.{5,200}$/.test(eleVal)) {
            $(ele).prev("label").html("توضیحات: حداقل پنج حرف.").css("color", "red");
            TrueOrFalse = false;
        }
        else $(ele).prev("label").html("توضیحات:").css("color", "#43a047");
    }

    // Email
    if (str === "email") {
        var reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!reg.test(eleVal)) {
            $(ele).prev("label").html("ایمیل: معتبر نمیباشد.").css("color", "red");
            TrueOrFalse = false;
        }

        else $(ele).prev("label").html("ایمیل:").css("color", "#43a047");
    }

    // Phone
    if (str === "phone") {
        if (!/^(09)\d{9}$/.test(eleVal)) {
            $(ele).prev("label").html("شماره همراه: معتبر نمیباشد.").css("color", "red");
            TrueOrFalse = false;
        }
        else $(ele).prev("label").html("شماره همراه:").css("color", "#43a047");
    }

    // FAQ Question
    if (str === "faq_ques") {
        if (!/^.{3,100}$/.test(eleVal)) {
            $(ele).prev("label").html("سوال: معتبر نمیباشد.").css("color", "red");
            TrueOrFalse = false;
        }
        else $(ele).prev("label").html("سوال:").css("color", "#43a047");
    }

    // FAQ Answer
    if (str === "faq_ans") {
        if (!/^.{3,1000}$/.test(eleVal)) {
            $(ele).prev("label").html("پاسخ: معتبر نمیباشد.").css("color", "red");
            TrueOrFalse = false;
        }
        else $(ele).prev("label").html("پاسخ:").css("color", "#43a047");
    }

    // Username
    if (str === "uname") {
        if (!/^(?=.{8,32}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$/.test(eleVal)) {
            $(ele).prev("label").html("نام کاربری: معتبر نمیباشد.").css("color", "red");
            TrueOrFalse = false;
        }
        else $(ele).prev("label").html("نام کاربری:").css("color", "#43a047");
    }

    // Password
    if (str === "password") {
        if (!/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,32}$/.test(eleVal)) {
            $(ele).prev("label").html("گذرواژه: معتبر نمیباشد").css("color", "red");
            $('span.passHint').addClass("show");
            TrueOrFalse = false;
        }
        else {
            $(ele).prev("label").html("گذرواژه:").css("color", "#43a047");
            $('span.passHint').removeClass("show");
        }
    }

    // Repeat Password
    if (str === "repass") {
        if (eleVal !== $("#passInp").val()) {
            $(ele).prev("label").html("تکرار گذرواژه: گذرواژه یکسان نمیباشد").css("color", "red");
            TrueOrFalse = false;
        }
        else $(ele).prev("label").html("تکرار گذرواژه:").css("color", "#43a047");
    }

    return TrueOrFalse;
}