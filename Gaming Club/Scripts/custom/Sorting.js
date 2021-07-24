//myid - reprsents the table column to get the td tag from 
//name - reprsents the id of the input html element
function filtertable(rownum, myid) {
    // Intialising variables

    var input = document.getElementById(myid);
    var filter = input.value.toUpperCase();
    var table = document.getElementById("sales");
    var tr = table.getElementsByTagName("tr");
    var td, i, txtValue;

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[rownum];
        //if there exists a table data, then fetch the text value within the td tag 
        if (td) {
            txtValue = td.textContent || td.innerText;

            //uses indexof to compare the string of the td value to the input string
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                //else hide the td element
                tr[i].style.display = "none";
            }
        }
    }
}
