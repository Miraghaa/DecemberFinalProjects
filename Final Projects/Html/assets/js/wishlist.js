if(localStorage.getItem('wishlists') === null){
    localStorage.setItem('wishlists',JSON.stringify([]))
  }
 
  let cart2_position = document.querySelector('.cart2-position')
  let a = document.querySelectorAll('.wishlist')
  for(let wl of a){
    wl.onclick = () =>{
      let id = wl.parentElement.parentElement.parentElement.getAttribute('id')
      console.log(id);
      let src = wl.parentElement.parentElement.querySelector('.Product-image img').getAttribute('src');
      let text = wl.parentElement.parentElement.parentElement.children[1].children[1].innerHTML
      let price = wl.parentElement.parentElement.parentElement.children[1].children[3].children[0].innerHTML
      let price_discount = wl.parentElement.parentElement.parentElement.children[1].children[3].children[1].innerHTML
      let wishlistss = JSON.parse(localStorage.getItem('wishlists'))
          let exist = wishlistss.find(x=> x.Id === id)
      
          if(exist === undefined){
            wishlistss.push({
                Id : id,
                sekil: src,
                yazi: text,
                price:price,
                price_discount:price_discount
            })
          }
          cart2_position.style.opacity = '1'
          cart2_position.style.left = '20px'
          setTimeout(() => {
          cart2_position.style.opacity = '0'
          cart2_position.style.left = '-200px'
          cart2_position.style.transition='.6s'
        }, 2000);
          localStorage.setItem('wishlists',JSON.stringify(wishlistss))
          show() 
    }
  }
  function show() {
    let basketa  = JSON.parse(localStorage.getItem('wishlists'))
    document.getElementById('count-wishlist').innerHTML = basketa.length
  }
  show()
  


function getems() {
    let items = JSON.parse(localStorage.getItem('wishlists'))
    let  n = ''
    for(let item of items){
        n+=`
        <div class="tab-wishlist-all col-lg-4 col-12" id="${item.Id}">
                    <div class="tab-wishlist">
                         <div class="wishlist-photo">
                            <a href="#"> <img src="${item.sekil}" alt=""></a>
                         </div>
                         <div class="wishlist-content ">
                            <p style="margin-left: 210px;"><i class="fa-solid fa-minus"></i></p>
                            <p style="font-size:20px;font-weight: bold">${item.yazi}</p>
                            <span style="color: #Ff6f2e; font-weight: 700">${item.price}</span>
                            <span style="text-decoration: line-through;margin-left: 10px; font-weight: 500;">${item.price_discount}</span>
                        </div>
                    </div>
                </div>
        `
    } 
    document.querySelector('.wishlist-all').innerHTML = n
    let dlt = document.querySelectorAll('.wishlist-content i')

for(let del of dlt){
   del.onclick = () => {
    let ID = del.parentElement.parentElement.parentElement.parentElement.getAttribute('id')
    let kartfilter = items.filter(z => z.Id != ID)
    localStorage.setItem('wishlists',JSON.stringify(kartfilter))
    getems()
    showWishlist()
    show()
   }
}
}

function showWishlist() {
    let karts = JSON.parse(localStorage.getItem('wishlists'))
    if(karts.length === 0){
        document.querySelector('.Heart').classList.remove('d-none')
     }
     else{
       document.querySelector('.Heart').classList.add('d-none')
     }
}

function show() {
    let basketa  = JSON.parse(localStorage.getItem('wishlists'))
    document.getElementById('count-wishlist').innerHTML = basketa.length
  }

  getems()
  showWishlist()
  show()


