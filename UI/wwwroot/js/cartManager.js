// Master menu (you can load this from API later)
const foodLookup = {
  1: { name: "Paneer Butter Masala", price: 220, rating: 4.5, popular: true },
  2: { name: "Veg Biryani", price: 180, rating: 4.2, popular: false },
  3: { name: "Chicken Tikka", price: 260, rating: 4.8, popular: true },
  4: { name: "Garlic Naan", price: 40, rating: 4.0, popular: false }
};

// Load and save cart in localStorage
function getCart() {
  return JSON.parse(localStorage.getItem("foodCart") || "{}");
}

function saveCart(cart) {
  localStorage.setItem("foodCart", JSON.stringify(cart));
}

// Add or remove quantity
function addToCart(id) {
  const cart = getCart();
  cart[id] = (cart[id] || 0) + 1;
  saveCart(cart);
}

function changeQty(id, delta) {
  const cart = getCart();
  cart[id] = Math.max(0, (cart[id] || 0) + delta);
  if (cart[id] === 0) delete cart[id];
  saveCart(cart);
}

// Total calculator with optional coupon and distance
function calculateTotals(cart, couponRate = 0, distance = 0) {
  let itemTotal = 0;
  for (const id in cart) {
    if (foodLookup[id]) {
      itemTotal += foodLookup[id].price * cart[id];
    }
  }

  const discount = itemTotal * couponRate;
  const delivery = distance <= 3 ? 30 : distance * 30;
  const final = itemTotal - discount + delivery;

  return {
    itemTotal,
    discount,
    delivery,
    final
  };
}

// Render utilities (optional depending on page)

// Render food card for restaurant page
function renderFoodCard(itemId, qty, containerId) {
  const item = foodLookup[itemId];
  const container = document.getElementById(containerId);
  const controlsId = `controls-${itemId}`;

  const card = document.createElement("div");
  card.className = "col-md-6 col-lg-4 mb-4";
  card.innerHTML = `
    <div class="card h-100 shadow-sm">
      <div class="card-body d-flex flex-column">
        <h5 class="card-title">${item.name}</h5>
        <p class="card-text mb-2">
          ₹${item.price} • ⭐ ${item.rating}
          ${item.popular ? '<span class="badge bg-warning text-dark ms-2">Highly Ordered</span>' : ''}
          <button class="btn btn-link btn-sm p-0 ms-2" onclick="showMoreInfo(${itemId})">More...</button>
        </p>
        <div class="mt-auto" id="${controlsId}">
          ${qty ? quantityControls(itemId, qty) : `<button class="btn btn-primary w-100" onclick="addToCartAndRender(${itemId})">Add to Cart</button>`}
        </div>
      </div>
    </div>
  `;

  container.appendChild(card);
}

// Controls UI for quantity (restaurant)
function quantityControls(id, qty) {
  return `
    <div class="d-flex justify-content-between align-items-center">
      <button class="btn btn-outline-secondary" onclick="changeQtyAndRender(${id}, -1)">−</button>
      <span>${qty}</span>
      <button class="btn btn-outline-secondary" onclick="changeQtyAndRender(${id}, 1)">+</button>
    </div>`;
}

// Wrapper to add + render (restaurant)
function addToCartAndRender(id) {
  addToCart(id);
  updateRestaurantItem(id);
  updateFloatingCart();
}

function changeQtyAndRender(id, delta) {
  changeQty(id, delta);
  updateRestaurantItem(id);
  updateFloatingCart();
}

function updateRestaurantItem(id) {
  const qty = getCart()[id] || 0;
  const controls = document.getElementById(`controls-${id}`);
  if (controls) {
    controls.innerHTML = qty ? quantityControls(id, qty) : `<button class="btn btn-primary w-100" onclick="addToCartAndRender(${id})">Add to Cart</button>`;
  }
}

function updateFloatingCart() {
  const cart = getCart();
  const totalItems = Object.values(cart).reduce((sum, qty) => sum + qty, 0);
  const btn = document.getElementById("cartBtn");
  if (btn) {
    document.getElementById("cartCount").innerText = totalItems;
    btn.classList.toggle("d-none", totalItems === 0);
  }
}

const sampleReviews = {
    1: [
        "Absolutely loved the Paneer Butter Masala, so creamy and flavorful!",
        "Best dish on the menu, highly recommended!",
        "Perfect spice level and rich taste."
    ],
    2: [
        "Veg Biryani was fragrant and perfectly cooked.",
        "A delightful treat for vegetarians.",
        "Full of flavor, just the right amount of spice."
    ],
    3: [
        "Chicken Tikka was juicy and well-marinated.",
        "Loved the smoky flavor, would order again!",
        "Super tender and delicious."
    ],
    4: [
        "Garlic Naan was soft and buttery.",
        "Pairs perfectly with curries!",
        "Crispy on the edges and packed with garlic flavor."
    ]
};


function showMoreInfo(itemId) {
    const item = foodLookup[itemId];
    if (!item) return;

    // Set dish name
    document.getElementById("modalDishName").textContent = item.name;

    // Clear user comment box
    const userCommentBox = document.getElementById("userComment");
    userCommentBox.value = "";

    // Load 2-3 random reviews
    const reviewsContainer = document.getElementById("reviewsList");
    reviewsContainer.innerHTML = ""; // clear existing

    const reviews = sampleReviews[itemId] || [];
    // Shuffle reviews and pick up to 3
    const shuffled = reviews.sort(() => 0.5 - Math.random());
    const selected = shuffled.slice(0, 3);

    selected.forEach(review => {
        const li = document.createElement("li");
        li.className = "list-group-item";
        li.textContent = review;
        reviewsContainer.appendChild(li);
    });

    // Show modal
    const modalElement = document.getElementById('moreInfoModal');
    const modal = new bootstrap.Modal(modalElement);
    modal.show();
}


document.getElementById("submitComment").addEventListener("click", () => {
    const commentBox = document.getElementById("userComment");
    const commentText = commentBox.value.trim();
    if (commentText === "") return; // ignore empty

    const reviewsContainer = document.getElementById("reviewsList");
    const li = document.createElement("li");
    li.className = "list-group-item list-group-item-primary"; // highlight user comment
    li.textContent = commentText;

    reviewsContainer.appendChild(li);
    commentBox.value = ""; // clear textbox
});

