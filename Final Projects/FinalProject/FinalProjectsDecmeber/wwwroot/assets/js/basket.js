if(localStorage.getItem('basket') === null){
  localStorage.setItem('basket',JSON.stringify([]))
}

let Add_cart = document.querySelectorAll('.Add-cart')
let cart_position = document.querySelector('.cart-position')
for(let add_cart of Add_cart){
  add_cart.onclick = () =>{
    let id = add_cart.parentElement.parentElement.getAttribute('id')
    let src = add_cart.parentElement.parentElement.querySelector('.Product-image img').getAttribute('src');
    let text = add_cart.parentElement.parentElement.children[1].children[1].innerText
    let price = add_cart.parentElement.parentElement.querySelector('.Product-about').children[3].children[0].innerHTML
    let baskets = JSON.parse(localStorage.getItem('basket'))
    let exist = baskets.find(x=> x.Id === id)
    
        if(exist === undefined){
          baskets.push({
              Id: id,
              sekil: src,
              yazi: text,
              price: price,
              Count: 1
          })
        }else {
          exist.Count += 1
        }
        cart_position.style.opacity = '1'
        cart_position.style.left = '20px'
        setTimeout(() => {
          cart_position.style.opacity = '0'
          cart_position.style.left = '-200px'
          cart_position.style.transition='.6s'
        }, 2000);
        localStorage.setItem('basket',JSON.stringify(baskets))
        showb()
        showbasket()  
        getbasket()
        calc()
   }
}

function getbasket() {
  let items = JSON.parse(localStorage.getItem('basket'))
  let  n = ''
  for(let item of items){
      n+=`
      <div class="tab-basket-all col-lg-12 col-12" id="${item.Id}">
                  <div class="tab-basket">
                       <div class="basket-photo">
                          <a href="#"> <img src="${item.sekil}" alt=""></a>
                       </div>
                       <div class="basket-content">
                          <p >${item.yazi}</p>
                          <span class="price1" style="color: #Ff6f2e; font-weight: 700;">${item.price}</span>
                          <span style="color: #Ff6f2e; font-weight: 700;">*</span>
                          <span class="price2" style="color: #Ff6f2e; font-weight: 700;">${item.Count}</span>
                          <div class="wrapper">
                            <span class="minus">-</span>
                            <span class="num">${item.Count}</span>
                            <span class="plus">+</span>
                          </div>
                          <p class="content-icon">remove</p>
                      </div>
                  </div>
              </div>
      `
  } 
  document.querySelector('.basket-all').innerHTML = n
  let dlt = document.querySelectorAll('.content-icon')
  const pluss = document.querySelectorAll('.plus'),
  minuss = document.querySelectorAll('.minus');

  let T = 1
  // for(let plus of pluss){
  //   if(T<15){
  //     plus.onclick =function(){
  //       let num = this.previousElementSibling
  //       let num2 = this.parentElement.previousElementSibling
  //       let num3 = this.parentElement.parentElement.children[1].innerText
  //       let priceValues = parseFloat(num3.replace('$', '')); 
  //       let b = num2.innerText
  //       let a = num.innerText
  //       if(a<15){
  //         a++;
  //         b++;
  //         num.innerText = a
  //         num2.innerText = b
  //       }
  //       let Totals = b*priceValues   
  //       document.getElementById('Total').innerHTML = Totals.toFixed(2)
  //       calc()
  //       T++;
  //     }
  //   }
  // }
  for (let plus of pluss) {
    if (T < 15) {
      plus.onclick = function () {
        let num = this.previousElementSibling;
        let num2 = this.parentElement.previousElementSibling;
        let num3 = this.parentElement.parentElement.children[1].innerText;
        let priceValues = parseFloat(num3.replace('$', ''));
        let b = num2.innerText;
        let a = num.innerText;
  
        if (a < 15) {
          a++;
          b++;
          num.innerText = a;
          num2.innerText = b;
  
          let ID = this.parentElement.parentElement.parentElement.parentElement.getAttribute('id');
          let items = JSON.parse(localStorage.getItem('basket'));
            let updatedItems = items.map(item => {
            if (item.Id == ID) {
              item.Count = a;
            }
            return item;
          });
            localStorage.setItem('basket', JSON.stringify(updatedItems));
        }
  
        let Totals = b * priceValues;
        document.getElementById('Total').innerHTML = Totals.toFixed(2);
        calc();
        T++;
      }
    }
  }
  for(let minus of minuss){
    minus.onclick =function(){
      let num = this.parentElement.children[1]
      let num2 = this.parentElement.previousElementSibling
      let num3 = this.parentElement.parentElement.children[1].innerText
      let priceValues = parseFloat(num3.replace('$', '')); 
      let b = num2.innerText
      let a = num.innerText
      if(a>1){
        a--;
        b--;
        num.innerText = a
        num2.innerText = b

        let ID = this.parentElement.parentElement.parentElement.parentElement.getAttribute('id');
          let items = JSON.parse(localStorage.getItem('basket'));
            let updatedItems = items.map(item => {
            if (item.Id == ID) {
              item.Count = a;
            }
            return item;
          });
            localStorage.setItem('basket', JSON.stringify(updatedItems));
      }
      let Total = b*priceValues
      document.getElementById('Total').innerHTML = Total.toFixed(2)
      calc()
    }
  }
  for(let del of dlt){
  del.onclick = () => {
    let ID = del.parentElement.parentElement.parentElement.getAttribute('id')
    let kartfilter = items.filter(z => z.Id != ID)
    localStorage.setItem('basket',JSON.stringify(kartfilter))
    getbasket()
    showb()
    calc()
    showbasket()
  }
  }
}
// function getproduct() {
//   let items = JSON.parse(localStorage.getItem('Productss'))
//   let  p = ''
//   for(let item of items){
//       p+=`
//       <div class="tab-basket-all col-lg-12 col-12" id="">
//                   <div class="tab-basket">
//                        <div class="basket-photo">
//                           <a href="#"> <img src="${item.sekil}" alt=""></a>
//                        </div>
//                        <div class="basket-content">
//                           <p >${item.yazi}</p>
//                           <span class="price1" style="color: #Ff6f2e; font-weight: 700;">${item.price}</span>
//                           <span style="color: #Ff6f2e; font-weight: 700;">*</span>
//                           <span class="price2" style="color: #Ff6f2e; font-weight: 700;">${item.Count}</span>
//                           <div class="wrapper">
//                             <span class="minus">-</span>
//                             <span class="num">${item.Count}</span>
//                             <span class="plus">+</span>
//                           </div>
//                           <p class="content-icon">remove</p>
//                       </div>
//                   </div>
//               </div>
//       `
//   } 
//   document.querySelector('.basket-all-product').innerHTML = p
//   let dlt = document.querySelectorAll('.content-icon')
//   const pluss = document.querySelectorAll('.plus'),
//   minuss = document.querySelectorAll('.minus');

//   let T = 1
//   for(let plus of pluss){
//     if(T<15){
//       plus.onclick =function(){
//         let num = this.previousElementSibling
//         let num2 = this.parentElement.previousElementSibling
//         let num3 = this.parentElement.parentElement.children[1].innerText
//         let priceValues = parseFloat(num3.replace('$', '')); 
//         let b = num2.innerText
//         let a = num.innerText
//         if(a<15){
//           a++;
//           b++;
//           num.innerText = a
//           num2.innerText = b
//         }
//         let Totals = b*priceValues   
//         document.getElementById('Total').innerHTML = Totals.toFixed(2)
//         calc()
//         T++;
//       }
//     }
//   }
//   for(let minus of minuss){
//     minus.onclick =function(){
//       let num = this.parentElement.children[1]
//       let num2 = this.parentElement.previousElementSibling
//       let num3 = this.parentElement.parentElement.children[1].innerText
//       let priceValues = parseFloat(num3.replace('$', '')); 
//       let b = num2.innerText
//       let a = num.innerText
//       if(a>1){
//         a--;
//         b--;
//         num.innerText = a
//         num2.innerText = b
//       }
//       let Total = b*priceValues
//       document.getElementById('Total').innerHTML = Total.toFixed(2)
//       calc()
//     }
//   }
//   for(let del of dlt){
//       del.onclick = () => {
//           let yazir = del.parentElement.children[0].innerText
//           let kartfilter = items.filter(z => z.yazi != yazir)
//           localStorage.setItem('Productss',JSON.stringify(kartfilter))
//           getproduct()
//             showb()
//             calc()
//             showbasket()
//       }
//   }
// }
function showb() {
  let basketb  = JSON.parse(localStorage.getItem('basket'))
  document.getElementById('count-basket').innerHTML = basketb.length
  document.getElementById('count-basket-cart').innerHTML = basketb.length
}
function calc(){
  var priceElements = document.querySelectorAll('.price1');
  Total= 0
  for (var i = 0; i < priceElements.length; i++) {
    let count = priceElements[i].parentElement.children[3].innerHTML
    var priceText = priceElements[i].innerHTML; 
    var priceValue = parseFloat(priceText.replace('$', '')); 
    let Totals = priceValue*count
    Total += Totals
  }
  document.getElementById('Total').innerHTML = Total.toFixed(2)
}
function showbasket() {
  let karts = JSON.parse(localStorage.getItem('basket'))
  if(karts.length === 0 ){
      document.querySelector('.basket-none').classList.remove('d-none')
      document.querySelector('.Total-price').classList.add('d-none')
   }
   else{
     document.querySelector('.basket-none').classList.add('d-none')
     document.querySelector('.Total-price').classList.remove('d-none')
   }
}

showbasket()
showb()
getbasket()
calc()
let basketaa = document.querySelector('.basket')
let basket_click = document.querySelector('#basket-click')
let _icon = document.querySelector('.basket-icon')

basket_click.onclick=()=>{
  basketaa.style.display = 'block'
}

_icon.onclick =() =>{
  if(basketaa.style.display === 'block'){
    basketaa.style.display = 'none'
  }
}


