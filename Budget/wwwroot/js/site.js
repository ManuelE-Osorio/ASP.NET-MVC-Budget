document.getElementById("nav-link-transactions").addEventListener('click', event => SwitchToTransactions(event))
document.getElementById("nav-link-categories").addEventListener('click', event => SwitchToCategories(event))

function SwitchToTransactions(event){
    debugger  //use event.target
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