import { defineStore, acceptHMRUpdate } from 'pinia'
import file from '@/api/file'

const useprojectst = defineStore('projectst', {
  state: () => ({
    defaultname: 'project',
    ext: '.bava',
    projectname: '',
    path: '',
    config: 'config.json',
    stepname: 'step.json',
    blocklyname: 'blockly.json',
    main: 'main.py',
    configcontent: '{}',
    stepcontent: '{}',
    blocklycontent: '{}',
    maincontent: '',
    datapath: 'data',
    binpath: 'bin',
    datachanged: false,
    version: 'V1.0.0'
  }),
  getters: {
    async getprojectpathasync() {
      return await file.combineasync(this.path, this.projectname + this.ext)
    },
    async getconfigpathasync() {
      return await file.combineasync(this.path, this.projectname + this.datapath, this.config)
    },
    async getsteppathasync() {
      return await file.combineasync(this.path, this.projectname + this.datapath, this.stepname)
    },
    async getblocklypathasync() {
      return await file.combineasync(this.path, this.projectname + this.datapath, this.blocklyname)
    },
    async getmainpathasync() {
      return await file.combineasync(this.path, this.projectname + this.binpath, this.main)
    },
    getdefaultname() {
      return this.defaultname
    },
    getext() {
      return this.ext
    },
    getchagned() {
      return this.datachanged
    },
    getversion() {
      return this.version
    }
  },
  actions: {
    setpath(path) {
      this.path = path
    },
    setprojectname(name) {
      this.projectname = name
    },
    setconfigcontent(content) {
      if (this.configcontent === content) return
      this.datachanged = true
      this.configcontent = content
    },
    setstepcontent(content) {
      if (this.stepcontent === content) return
      this.datachanged = true
      this.stepcontent = content
    },
    setblocklycontent(content) {
      if (this.blocklycontent === content) return
      this.datachanged = true
      this.blocklycontent = content
    },
    setmaincontent(content) {
      if (this.maincontent === content) return
      this.datachanged = true
      this.maincontent = content
    },
    reset() {
      this.projectname = ''
      this.path = ''
      this.configcontent = '{}'
      this.stepcontent = '{}'
      this.blocklycontent = '{}'
      this.maincontent = ''
    },
    async saveasync() {
      await file.saveasync(await this.getprojectpathasync, this.getversion)
      await file.saveasync(await this.getconfigpathasync, this.configcontent)
      await file.saveasync(await this.getsteppathasync, this.stepcontent)
      await file.saveasync(await this.getblocklypathasync, this.blocklycontent)
      await file.saveasync(await this.getmainpathasync, this.maincontent)
      this.datachanged = false
    },
    async readasync() {
      this.configcontent = await file.readasync(this.getconfigpath)
      this.stepcontent = await file.readasync(this.getsteppath)
      this.blocklycontent = await file.readasync(this.getblocklypath)
      this.maincontent = await file.readasync(this.getmainpath)
      this.datachanged = false
    },
    async newprojectasync(name) {
      let res = await file.newasync(name, "valve (*" + this.ext + ")|*" + this.ext, this.getdefaultname)
      if (res === null) return
      this.reset()
      this.path = await file.pathasync(res);
      this.projectname = await file.nameasync(res);
      await this.saveasync()
    },
    async openprojectasync(path) {
      this.reset()
      this.path = await file.pathasync(path)
      this.projectname = await file.nameasync(path)
      this.readasync()
    }
  }
})

export default useprojectst

if (import.meta.hot) {
  import.meta.hot.accept(acceptHMRUpdate(useprojectst, import.meta.hot))
}
