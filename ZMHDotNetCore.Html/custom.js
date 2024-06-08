function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}

function successMsg(msg) {
    Swal.fire({
        title: "Good job",
        text: msg,
        icon: "success"
    });
}

function errorMsg(msg) {
    Swal.fire({
        title: "Opps sry!",
        text: msg,
        icon: "error"
    });
}

function confirmMsg(msg) {
    let result = new Promise(function (success, error) {
        Swal.fire({
            title: "Confirm",
            text: msg,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (!result.isConfirmed) error();

            success();
        });
    });
    return result;
}