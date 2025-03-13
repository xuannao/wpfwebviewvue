const server = {
  async post(url, data) {
    try {
      let res = await fetch('/api' + url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
      })
      if (res.ok) {
        return await res.json()
      } else {
        return null
      }
    } catch (error) {
      console.log(error)
      return null
    }
  },
  async get(url) {
    try {
      let res = await fetch('/api' + url, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json'
        }
      })
      if (res.ok) {
        return await res.json()
      } else {
        return null
      }
    } catch (error) {
      console.log(error)
      return null
    }
  }
}
export default server
