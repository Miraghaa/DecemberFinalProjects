
function getcheckout(){
    let items = JSON.parse(localStorage.getItem('basket'))
    let  b = ''
    for(let item of items){
        b+=`
        <div class="tab-basket-all col-lg-12 col-12">
                    <div class="tab-basket">
                         <div class="basket-photo">
                            <a href="#"> <img src="${item.sekil}" alt=""></a>
                         </div>
                         <div class="basket-content">
                            <p >${item.yazi}</p>
                            <span class="price2">${item.Count}</span>
                            <span class="price11">${item.price}</span>
                        </div>
                    </div>
                </div>
        `
    } 
    
    document.querySelector('.order-all').innerHTML = b
  }
 getcheckout()

 calc2()
function calc2(){
  var priceElementss = document.querySelectorAll('.price11');
  
  Totalss= 0
  
  for (var i = 0; i < priceElementss.length; i++) {
    let counts = priceElementss[i].previousElementSibling.innerHTML
    console.log(counts);
    var priceTexts = priceElementss[i].innerHTML; 
    var priceValues = parseFloat(priceTexts.replace('$', '')); 
    let Totalsss = priceValues*counts
    Totalss += Totalsss
  }
  document.querySelector('#Totals').innerHTML = Totalss.toFixed(2)
}