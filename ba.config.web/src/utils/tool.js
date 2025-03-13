const tool = {
  theme: {
    get(defaultTheme) {
      let theme = localStorage.getItem('tool_theme')
      if (theme) {
        return theme === 'lightTheme' ? null : defaultTheme
      } else {
        return defaultTheme
      }
    },
    set(theme) {
      if (theme === 'darkTheme') {
        localStorage.setItem('tool_theme', 'darkTheme')
      } else {
        localStorage.setItem('tool_theme', 'lightTheme')
      }
    },
  },
  language: {
    get() {
      let language = localStorage.getItem('tool_language')
      if (language) {
        return language
      } else {
        return null
      }
    },
    set(language) {
      localStorage.setItem('tool_language', language)
    },
  },
  pathname: {
    get() {
      let pathname = sessionStorage.getItem('tool_pathname')
      if (pathname) {
        return pathname
      } else {
        return null
      }
    },
    set(pathname) {
      sessionStorage.setItem('tool_pathname', pathname)
    },
  }
}
export default tool
