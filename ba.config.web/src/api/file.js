import server from "./server"

const file = {
  async newasync(title, filter, filename) {
    if (!title || !filter || !filename) {
      return null
    }
    let data = {
      title: title,
      filter: filter,
      filename: filename
    }
    let res = await server.post("/file/new", data)
    if (res && res.status) {
      return res.data
    } else {
      return null
    }
  },
  async openasync(title, filter) {
    if (!title || !filter) {
      return null
    }
    let data = {
      title: title,
      filter: filter
    }
    let res = await server.post("/file/open", data)
    if (res && res.status) {
      return res.data
    } else {
      return null
    }
  },
  async saveasync(path, content) {
    if (!path) {
      return null
    }
    let data = {
      path: path,
      content: content
    }
    let res = await server.post("/file/save", data)
    if (res && res.status) {
      return res.data
    } else {
      return null
    }
  },
  async readasync(path) {
    if (!path) {
      return null
    }
    let data = {
      path: path
    }
    let res = await server.post("/file/read", data)
    if (res && res.status) {
      return res.data
    } else {
      return null
    }
  },
  async nameasync(path) {
    if (!path) {
      return null
    }
    let data = {
      path: path
    }
    let res = await server.post("/file/name", data)
    if (res && res.status) {
      return res.data
    } else {
      return null
    }
  },
  async extasync(path) {
    if (!path) {
      return null
    }
    let data = {
      path: path
    }
    let res = await server.post("/file/ext", data)
    if (res && res.status) {
      return res.data
    } else {
      return null
    }
  },
  async pathasync(path) {
    if (!path) {
      return null
    }
    let data = {
      path: path
    }
    let res = await server.post("/file/path", data)
    if (res && res.status) {
      return res.data
    } else {
      return null
    }
  },
  async combineasync(path1, path2, path3 = "", path4 = "") {
    if (!path1 || !path2) {
      return null
    }
    let data = {
      path1: path1,
      path2: path2,
      path3: path3,
      path4: path4
    }
    let res = await server.post("/file/combine", data)
    if (res && res.status) {
      return res.data
    } else {
      return null
    }
  }
}
export default file
