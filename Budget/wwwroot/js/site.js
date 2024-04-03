document.getElementById("nav-link-transactions").addEventListener('click', event => SwitchToTransactions(event))
document.getElementById("nav-link-categories").addEventListener('click', event => SwitchToCategories(event))
document.getElementById('edit-transaction-modal').querySelector('form').addEventListener('submit', event => transactionFetch(event))

function SwitchToTransactions(event){
     //use event.target
    const transactions = document.getElementById("nav-link-transactions")
    const transactionsTable = document.getElementById("transactions-table")
    const categories = document.getElementById("nav-link-categories")
    const categoriesTable = document.getElementById("categories-table")
    const attrib = transactions.getAttribute('class')
    if (!attrib.includes('active'))
    {
        transactions.setAttribute('class', 'nav-link active')
        categories.setAttribute('class', 'nav-link')
        transactionsTable.hidden = false
        categoriesTable.hidden = true
    }
}

function SwitchToCategories(event){
      //use event.target
    const transactions = document.getElementById("nav-link-transactions")
    const transactionsTable = document.getElementById("transactions-table")
    const categories = document.getElementById("nav-link-categories")
    const categoriesTable = document.getElementById("categories-table")
    const attrib = categories.getAttribute('class')
    if (!attrib.includes('active'))
    {
        categories.setAttribute('class', 'nav-link active')
        transactions.setAttribute('class', 'nav-link')
        transactionsTable.hidden = true
        categoriesTable.hidden = false
    }
}


function transactionFetch(event){
    event.preventDefault()
    const formData = new FormData(event.target)
    const transaction = Object.fromEntries(formData);
    if(transaction.id == 0)
    {
        createTransaction(transaction)
        console.log('fetching')
    }
    else
    {
        updateTransaction(transaction)
        console.log('fetching')
    }
}

function updateTransaction(transaction)
{
    debugger
    delete transaction.__Invariant
    const apiAddress = `Budget/Transactions/Update/${transaction.Id}`  
    console.log(JSON.stringify(transaction))
    fetch(apiAddress,
    {
        method: 'PUT',
        headers: {
            'Accept' : '*/*' ,
            'Content-Type' : 'application/json'},
        body: JSON.stringify(transaction)
    })
    .then( response => {
        if(response.status == 200){
            return response.json()
        }
        else{
            throw new Error('Server error, please try again later.')
        }
    })
    .then( body => {
        console.log(body)
        // document.getElementById(`accordion${body.id}`).replaceWith(generateAccordion(body))
    })
    .catch( e => {
        window.alert(e)
        console.log('Catch', e)
    })
}

function deleteTransactionModal(id){
    console.log(id)
}

function transactionModal(id){
    const element = document.getElementById('edit-transaction-modal')
    const modal = bootstrap.Modal.getOrCreateInstance(element)
    // const input = element.getElementsByTagName('input')
    // const category = element.querySelector('#Category')
    // const data = document.getElementById(`transaction-${id}`)
    // const values = data.getElementsByTagName('td')
    // input[0].value = id
    // input[1].value = values[0].innerText
    // input[2].value = values[1].innerText
    // input[3].valueAsDate = new Date(Date.parse(values[2].innerText))
    // input[5].value = values[3].innerText
    // category.value = values[4].innerText + 'hola'  //pending create or delete
    modal.show()
}
